using UnityEngine;
using System.Collections.Generic;

namespace MoleBash
{
    // Make sure you set the texture to 'wrap' not 'clamp' in the import settings

    //[ExecuteInEditMode]
    public class PaletteCycle : MonoBehaviour
    {
        public int Palette = 0;

        public int PaletteCount = 5;

        List<Material> materials;

        void Start()
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            SkinnedMeshRenderer[] skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            materials = new List<Material>();
            
            foreach (MeshRenderer renderer in renderers)
            {
                if (!materials.Contains(renderer.sharedMaterial))
                {
                    materials.Add(renderer.material);
                }
            }
            
            foreach (SkinnedMeshRenderer renderer in skinnedRenderers)
            {
                if (!materials.Contains(renderer.sharedMaterial))
                {
                    materials.Add(renderer.material);
                }
            }
        }

        void Update()
        {
            foreach (Material mat in materials)
            {
                mat.mainTextureOffset = new Vector2(Palette / (float)PaletteCount, 0);
            }
        }
    }
}

