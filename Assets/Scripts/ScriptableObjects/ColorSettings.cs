using System;
using System.Collections.Generic;
using Core.Entities;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "AffiliationSettings", menuName = "AffiliationSettings", order = 1)]
    public class ColorSettings : ScriptableObject
    {
        [SerializeField] private List<Habitat> _habitats;

        public Material GetMaterial(int colorID, TypeEntity typeEntity)
        {
            var habitat = _habitats[0].bindingPlayersMaterials;

            List<BindingMaterial> bindingMaterial = new List<BindingMaterial>();
            
            foreach (var item in habitat)
            {
                if (colorID == item.colorID)
                {
                    bindingMaterial = item.bindingMaterials;
                    break;
                }
            }

            Material targetMaterial = null;
            
            foreach (var item in bindingMaterial)
            {
                if (item.type == typeEntity)
                {
                    targetMaterial = item.material;
                }
            }

            return targetMaterial;
        }
    }

    [Serializable]
    public class Habitat
    {
        public string nameHabitat;
        public List<BindingPlayersMaterial> bindingPlayersMaterials;
    }

    [Serializable]
    public class BindingPlayersMaterial
    {
        public int colorID;
        public List<BindingMaterial> bindingMaterials;
    }
    
    [Serializable]
    public class BindingMaterial
    {
        public TypeEntity type;
        public Material material;
    }
}