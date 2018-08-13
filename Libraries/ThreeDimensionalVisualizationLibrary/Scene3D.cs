using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThreeDimensionalVisualizationLibrary.Objects;

namespace ThreeDimensionalVisualizationLibrary
{
    public class Scene3D
    {
        private List<Object3D> objectList = null;
        private List<Light> lightList = null;

        public Scene3D()
        {
            objectList = new List<Object3D>();
            lightList = new List<Light>();
        }

        // NOTE: does not handle nested objects!
        public void SortForTranslucence()
        {
            objectList.Sort((a, b) => a.Alpha.CompareTo(b.Alpha));
            objectList.Reverse();
        }

        public Object3D GetObject(string name)
        {
            Object3D foundObject = null;
            foreach (Object3D object3D in objectList)
            {
                foundObject = object3D.FindObject(name);
                if (foundObject != null) { break; }
            }
            return foundObject;
        }

        public Boolean AnyLightOn()
        {
            Light firstLightOn = lightList.Find(l => l.IsOn == true);
            if (firstLightOn == null) { return false; }
            else { return true; }
        }

        public void AddObject(Object3D object3D)
        {
            if (objectList == null) { objectList = new List<Object3D>(); }
            objectList.Add(object3D);
        }

        public List<Object3D> ObjectList
        {
            get { return objectList; }
        }

        public List<Light> LightList
        {
            get { return lightList; }
            set { lightList = value; }
        }
    }
}
