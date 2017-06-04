using UnityEngine;
using Vuforia;
using System.Collections.Generic;
using System.Linq;

namespace MoleBash
{
    public enum TrackingState
    {
        LowQuality,
        MedQuality,
        HighQuality
    };

    public class VuforiaController : MonoBehaviour, IUserDefinedTargetEventHandler
    {
        private UserDefinedTargetBuildingBehaviour TargetBuilder;
        private ObjectTracker ObjectTracker;
        private DataSet BuiltDataSet;
        
        public ImageTargetBehaviour ImageTargetTemplate;

        public ImageTargetBehaviour TargetCopy { get; private set; }

        public TrackingState TrackingState { get; private set; }
        
        public bool HasTarget { get { return TargetCopy != null; } }

        void Start()
        {
            TargetBuilder = GetComponent<UserDefinedTargetBuildingBehaviour>();
            TargetBuilder.RegisterEventHandler(this);
            TrackingState = TrackingState.LowQuality;
        }

        public void BuildTarget()
        {
            if (TrackingState == TrackingState.HighQuality || TrackingState == TrackingState.MedQuality)
            {
                // create the name of the next target.
                // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
                string targetName = string.Format("{0}-1", ImageTargetTemplate.TrackableName);

                // generate a new target:
                TargetBuilder.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);
            }
        }

        public void DestroyTarget()
        {
            ObjectTracker.DeactivateDataSet(BuiltDataSet);
            ObjectTracker.Stop();
            BuiltDataSet.DestroyAllTrackables(true);
            
            Destroy(TargetCopy);
        }

        public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
        {
            switch (frameQuality)
            {
                case ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH:
                    TrackingState = TrackingState.HighQuality;
                    break;
                case ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM:
                    TrackingState = TrackingState.MedQuality;
                    break;
                case ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW:
                    TrackingState = TrackingState.LowQuality;
                    break;
            }
        }
        
        public void OnInitialized()
        {
            ObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            if (ObjectTracker != null)
            {
                // Create a new dataset
                BuiltDataSet = ObjectTracker.CreateDataSet();
                ObjectTracker.ActivateDataSet(BuiltDataSet);
            }
        }

        public void OnNewTrackableSource(TrackableSource trackableSource)
        {
            // Deactivates the dataset first
            ObjectTracker.DeactivateDataSet(BuiltDataSet);
            
            BuiltDataSet.DestroyAllTrackables(true);
            
            // Get predefined trackable and instantiate it
            TargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
            TargetCopy.gameObject.name = "UserDefinedTarget-1";

            // Add the duplicated trackable to the data set and activate it
            BuiltDataSet.CreateTrackable(trackableSource, TargetCopy.gameObject);

            // Activate the dataset again
            ObjectTracker.ActivateDataSet(BuiltDataSet);

            // Extended Tracking with user defined targets only works with the most recently defined target.
            // If tracking is enabled on previous target, it will not work on newly defined target.
            // Don't need to call this if you don't care about extended tracking.
            RestartExtendedTracking();
            ObjectTracker.Stop();
            ObjectTracker.ResetExtendedTracking();
            ObjectTracker.Start();
        }

        private void RestartExtendedTracking()
        {
            StateManager stateManager = TrackerManager.Instance.GetStateManager();
            List<TrackableBehaviour> trackableList = stateManager.GetTrackableBehaviours().ToList();
            if (trackableList.Count == 1) {
                ImageTargetBehaviour currentItb = trackableList[0] as ImageTargetBehaviour;
                currentItb.ImageTarget.StopExtendedTracking();
                currentItb.ImageTarget.StartExtendedTracking();
            }
        }
    }
}
