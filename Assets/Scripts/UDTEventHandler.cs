/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;
using MoleBash;

public class UDTEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
    public ImageTargetBehaviour ImageTargetTemplate;
    
    private UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;
    private ObjectTracker mObjectTracker;
    
    // DataSet that newly defined targets are added to
    private DataSet mBuiltDataSet;
    
    // Currently observed frame quality
    private ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;
    
    public void Start()
    {
        mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        mTargetBuildingBehaviour.RegisterEventHandler(this);   
    }

    public void OnInitialized ()
    {
        mObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (mObjectTracker != null)
        {
            // Create a new dataset
            mBuiltDataSet = mObjectTracker.CreateDataSet();
            mObjectTracker.ActivateDataSet(mBuiltDataSet);
        }
    }
    

    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        mFrameQuality = frameQuality;
        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)
        {
            Debug.Log("Low camera image quality");
        }
    }
    

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        // Deactivates the dataset first
        mObjectTracker.DeactivateDataSet(mBuiltDataSet);

        mBuiltDataSet.DestroyAllTrackables(true);

        // Get predefined trackable and instantiate it
        var mTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
        mTargetCopy.gameObject.name = "UserDefinedTarget-1";

        // Add the duplicated trackable to the data set and activate it
        mBuiltDataSet.CreateTrackable(trackableSource, mTargetCopy.gameObject);

        // Activate the dataset again
        mObjectTracker.ActivateDataSet(mBuiltDataSet);

        // Extended Tracking with user defined targets only works with the most recently defined target.
        // If tracking is enabled on previous target, it will not work on newly defined target.
        // Don't need to call this if you don't care about extended tracking.
        StopExtendedTracking();
        mObjectTracker.Stop();
        mObjectTracker.ResetExtendedTracking();
        mObjectTracker.Start();
    }

    
    public void BuildNewTarget()
    {
        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM || 
            mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            // create the name of the next target.
            // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
            string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, 1);

            // generate a new target:
            mTargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);
        }
    }


    private void StopExtendedTracking()
    {
        StateManager stateManager = TrackerManager.Instance.GetStateManager();
        List<TrackableBehaviour> trackableList = stateManager.GetTrackableBehaviours().ToList();
        if (trackableList.Count == 1)
        {
            ImageTargetBehaviour currentItb = trackableList[0] as ImageTargetBehaviour;
            currentItb.ImageTarget.StopExtendedTracking();
            currentItb.ImageTarget.StartExtendedTracking();
        }
    }
}



