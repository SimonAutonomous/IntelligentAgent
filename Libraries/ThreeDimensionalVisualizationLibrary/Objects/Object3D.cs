using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using MathematicsLibrary.Geometry;

namespace ThreeDimensionalVisualizationLibrary.Objects
{
    [DataContract]
    public abstract class Object3D
    {
        protected string name = null;
        protected List<Vertex3D> vertexList = null;
        protected List<TriangleIndices> triangleIndicesList = null;
        protected List<Vector3D> triangleNormalVectorList = null;
        protected List<Object3D> object3DList = null; // Allows nested objects (but not that transparency sorting will not work).

        private float[] ambientColor = new float[] { 1f, 1f, 1f, 1f };
        private float[] diffuseColor = new float[] { 1f, 1f, 1f, 1f };
        private float[] specularColor = new float[] { 1f, 1f, 1f, 1f };
        private int shininess;

        protected int textureID = -1;

        protected System.Boolean visible = true;
        protected float vertexSize = 1f;
        protected float[] vertexColor = new float[] { 1f, 1f, 1f, 1f };
        protected float alpha = 1f;
        protected float wireFrameWidth = 1f;
        protected ShadingModel shadingModel = ShadingModel.Smooth;
        protected System.Boolean useLight = true;
        protected System.Boolean showVertices = false;
        protected System.Boolean showWireFrame = false;
        protected System.Boolean showSurfaces = true;

        // 20161011
        protected double[] position = new double[] { 0f, 0f, 0f };
        protected double[] rotation = new double[] { 0f, 0f, 0f };

        public Object3D()
        {
            name = "";
            vertexList = new List<Vertex3D>();
            triangleIndicesList = new List<TriangleIndices>();
            textureID = -1;
            ambientColor = new float[] { 1f, 1f, 1f, 1f };
            diffuseColor = new float[] { 1f, 1f, 1f, 1f };
            specularColor = new float[] { 1f, 1f, 1f, 1f };
        }

        public virtual void Generate(List<double> parameterList)
        {
            vertexList = new List<Vertex3D>();
            triangleIndicesList = new List<TriangleIndices>();
            position = new double[] { 0f, 0f, 0f };
            rotation = new double[] { 0f, 0f, 0f };
        }

        public void SetAlpha(float alpha)
        {
            this.alpha = alpha;
            if (alpha < 0) { alpha = 0; }
            else if (alpha > 1) { alpha = 1; }
            byte byteAlpha = (byte)Math.Round(255 * alpha);
            foreach (Vertex3D vertex in vertexList)
            {
                vertex.Color = Color.FromArgb(byteAlpha, vertex.Color.R, vertex.Color.G, vertex.Color.B);
            }
            ambientColor[3] = alpha;
            diffuseColor[3] = alpha;
            specularColor[3] = alpha;
        }

        protected void GenerateTriangleConnectionLists()
        {
            for (int iTriangle = 0; iTriangle < triangleIndicesList.Count; iTriangle++)
            {
                TriangleIndices triangleIndices = triangleIndicesList[iTriangle];
                vertexList[triangleIndices.Index1].TriangleConnectionList.Add(iTriangle);
                vertexList[triangleIndices.Index2].TriangleConnectionList.Add(iTriangle);
                vertexList[triangleIndices.Index3].TriangleConnectionList.Add(iTriangle);
            }
        }

        // This method computes the normal vector for each triangle, and assigns the
        // same normal vector to each vertex in the triangle. 
        // Suitable for flat shading, but not for smooth shading ZZZ
        public void ComputeTriangleNormalVectors()
        {
            triangleNormalVectorList = new List<Vector3D>();
            for (int iTriangle = 0; iTriangle < triangleIndicesList.Count; iTriangle++)
            {
                TriangleIndices triangleIndices = triangleIndicesList[iTriangle];
                Vector3D firstVector = Vector3D.FromPoints(vertexList[triangleIndices.Index2].Position, vertexList[triangleIndices.Index1].Position);
                Vector3D secondVector = Vector3D.FromPoints(vertexList[triangleIndices.Index3].Position, vertexList[triangleIndices.Index1].Position);
                Vector3D normalVector = Vector3D.Cross(firstVector, secondVector);
                normalVector.Normalize();
                triangleNormalVectorList.Add(normalVector);
                vertexList[triangleIndices.Index1].NormalVector = normalVector;
                vertexList[triangleIndices.Index2].NormalVector = normalVector;
                vertexList[triangleIndices.Index3].NormalVector = normalVector;
            }
        }

        // This method computes the normal vector for each vertex, by averaging over
        // the normal vectors of the triangles to which the vertex belongs.
        // Suitable for smooth shading.
        public void ComputeVertexNormalVectors()
        {
            for (int iVertex = 0; iVertex < vertexList.Count; iVertex++)
            {
                Vertex3D vertex = vertexList[iVertex]; 
                Vector3D vertexNormalVector = new Vector3D();
                for (int ii = 0; ii < vertex.TriangleConnectionList.Count; ii++)
                {
                    int triangleIndex = vertex.TriangleConnectionList[ii];
                    vertexNormalVector.Add(triangleNormalVectorList[triangleIndex]);
                }
                vertexNormalVector.Normalize();
                vertex.NormalVector = vertexNormalVector;
            }
        }

        public void SetUniformColor(Color color)
        {
            foreach (Vertex3D vertex in vertexList)
            {
                vertex.Color = color;
            }
        }

        // Do not use (superseded by Move(...); see below)
        // Translate(...) retained for back compatibility only.
    /*    public void Translate(double deltaX, double deltaY, double deltaZ)
        {
            foreach (Vertex3D vertex in vertexList)
            {
                vertex.Position.X += deltaX;
                vertex.Position.Y += deltaY;
                vertex.Position.Z += deltaZ;
            }
            if (object3DList != null)
            {
                foreach (Object3D object3D in object3DList) { object3D.Translate(deltaX, deltaY, deltaZ); }
            }  
        }    */  

        private double GetXCenter()
        {
            double xCenter = 0;
            foreach (Vertex3D vertex in vertexList)
            {
                xCenter += vertex.Position.X;
            }
            xCenter /= vertexList.Count;
            return xCenter;
        }

        private double GetYCenter()
        {
            double yCenter = 0;
            foreach (Vertex3D vertex in vertexList)
            {
                yCenter += vertex.Position.Y;
            }
            yCenter /= vertexList.Count;
            return yCenter;
        }

        private double GetZCenter()
        {
            double zCenter = 0;
            foreach (Vertex3D vertex in vertexList)
            {
                zCenter += vertex.Position.Z;
            }
            zCenter /= vertexList.Count;
            return zCenter;
        }

        // 20161010 NEW
        public void Move(double deltaX, double deltaY, double deltaZ)
        {
            if (position == null) { position = new double[] { 0f, 0f, 0f }; } // Needed in case of deserialization.
            position[0] += deltaX;
            position[1] += deltaY;
            position[2] += deltaZ;
        }

        // 20161010 NEW
        public void RotateX(double deltaRotationX)
        {
            rotation[0] += deltaRotationX;
        }

        // 20161010 NEW
        public void RotateY(double deltaRotationY)
        {
            rotation[1] += deltaRotationY;
        }

        // 20161010 NEW
        public void RotateZ(double deltaRotationZ)
        {
            rotation[2] += deltaRotationZ;
        }

        public Object3D FindObject(string name)
        {
            Object3D foundObject = null;
            if (this.name == name) { return this; }
            else
            {
                if (object3DList != null)
                {
                    foreach (Object3D object3D in this.object3DList)
                    {
                        foundObject = object3D.FindObject(name);
                        if (foundObject != null)
                        {
                            break;
                        }
                    }
                }
                return foundObject;
            }
        }

        private void RenderVertices()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
            System.Boolean lightEnabled = GL.IsEnabled(EnableCap.Lighting);
            if (lightEnabled) { GL.Disable(EnableCap.Lighting); }
            float previousPointSize = GL.GetFloat(GetPName.PointSize);
            GL.PointSize(vertexSize);
            GL.Enable(EnableCap.PointSmooth);
            GL.Begin(PrimitiveType.Points);
            for (int ii = 0; ii < vertexList.Count; ii++)
            {
                GL.Color3(vertexList[ii].Color.R, vertexList[ii].Color.G, vertexList[ii].Color.B);  // 20161118
                GL.Vertex3(vertexList[ii].Position.X, vertexList[ii].Position.Z, -vertexList[ii].Position.Y);
            }
            GL.End();
            GL.PointSize(previousPointSize);
            if (lightEnabled) { GL.Enable(EnableCap.Lighting); }
        }

        private void RenderWireFrame()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.Disable(EnableCap.Lighting);
            //   GL.Disable(EnableCap.DepthTest);
            //  GL.Enable(EnableCap.CullFace);
            System.Boolean useLightState = useLight;
            useLight = false;
            float previousWireFrameWidth = GL.GetFloat(GetPName.LineWidth);
            GL.LineWidth(wireFrameWidth);
            RenderTriangles();
            GL.LineWidth(previousWireFrameWidth);
            useLight = useLightState;
            //   GL.Disable(EnableCap.CullFace);
            //    GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
        }

        private void RenderSurfaces()
        {
            if (textureID > 0)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, Convert.ToInt32(TextureWrapMode.Repeat));
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, Convert.ToInt32(TextureWrapMode.Repeat));
            }
            if (useLight)
            {
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuseColor);
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambientColor);
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specularColor);
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, shininess);
            }
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            if (useLight) { GL.Enable(EnableCap.Lighting); }
            else { GL.Disable(EnableCap.Lighting); }
            RenderTriangles();
        }

        private void RenderTriangles()
        {
            for (int iTriangle = 0; iTriangle < triangleIndicesList.Count; iTriangle++)
            {
                TriangleIndices triangleIndices = triangleIndicesList[iTriangle];
                Vertex3D vertex1 = vertexList[triangleIndices.Index1];
                Vertex3D vertex2 = vertexList[triangleIndices.Index2];
                Vertex3D vertex3 = vertexList[triangleIndices.Index3];
                Vector3D normal1 = vertex1.NormalVector;
                Vector3D normal2 = vertex2.NormalVector;
                Vector3D normal3 = vertex3.NormalVector;
                Vector3D triangeNormal = triangleNormalVectorList[iTriangle];
                GL.Begin(PrimitiveType.Triangles);
                if (textureID > 0) { GL.BindTexture(TextureTarget.Texture2D, textureID); }
                if (shadingModel == ShadingModel.Flat) { GL.Normal3(triangeNormal.X, triangeNormal.Z, -triangeNormal.Y); }
                if (shadingModel == ShadingModel.Smooth) { GL.Normal3(normal1.X, normal1.Z, -normal1.Y); }
                if (!useLight) { GL.Color4(vertex1.Color.R, vertex1.Color.G, vertex1.Color.B, vertex1.Color.A); }
                if (textureID > 0) { GL.TexCoord2(vertex1.TextureCoordinates.X, vertex1.TextureCoordinates.Y); }
                GL.Vertex3(vertex1.Position.X, vertex1.Position.Z, -vertex1.Position.Y);
                if (shadingModel == ShadingModel.Smooth) { GL.Normal3(normal2.X, normal2.Z, -normal2.Y); }
                if (!useLight) { GL.Color4(vertex2.Color.R, vertex2.Color.G, vertex2.Color.B, vertex2.Color.A); }
                if (textureID > 0)
                {
                    if (vertex2.TextureCoordinates.X < vertex1.TextureCoordinates.X)
                    {
                        GL.TexCoord2(vertex2.TextureCoordinates.X + 1, vertex2.TextureCoordinates.Y);
                    }
                    else { GL.TexCoord2(vertex2.TextureCoordinates.X, vertex2.TextureCoordinates.Y); }
                }
                GL.Vertex3(vertex2.Position.X, vertex2.Position.Z, -vertex2.Position.Y);
                if (shadingModel == ShadingModel.Smooth) { GL.Normal3(normal3.X, normal3.Z, -normal3.Y); }
                if (!useLight) { GL.Color4(vertex3.Color.R, vertex3.Color.G, vertex3.Color.B, vertex3.Color.A); }
                if (textureID > 0)
                {
                    if (vertex3.TextureCoordinates.X < vertex1.TextureCoordinates.X)
                    {
                        GL.TexCoord2(vertex3.TextureCoordinates.X + 1, vertex3.TextureCoordinates.Y);
                    }
                    else { GL.TexCoord2(vertex3.TextureCoordinates.X, vertex3.TextureCoordinates.Y); }
                }
                GL.Vertex3(vertex3.Position.X, vertex3.Position.Z, -vertex3.Position.Y);
                GL.End();
            }
        }

        public void Render()
        {
            if (!visible) { return; }

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.Translate(position[0], position[2], -position[1]);
            GL.Rotate(rotation[2], new Vector3d(0f, 1f, 0f)); 
            GL.Rotate(rotation[1], new Vector3d(0f, 0f, -1f)); 
            GL.Rotate(rotation[0], new Vector3d(1f, 0f, 0f));   

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha); 
            if (alpha < 1) GL.Enable(EnableCap.Blend); 
            if (showSurfaces) { RenderSurfaces(); }
            if (showWireFrame) { RenderWireFrame(); }
            if (showVertices) { RenderVertices(); }
            if (alpha < 1) GL.Disable(EnableCap.Blend);
            if (object3DList != null)
            {
                foreach (Object3D object3D in object3DList)
                {
                    object3D.Render();
                }
            }
            GL.PopMatrix();
        }

        // 20160617
        public void SetTexture(int textureID)
        {
            this.textureID = textureID;
        }

        [DataMember]
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        [DataMember]
        public Color AmbientColor
        {
            get 
            {
                Color color = Color.FromArgb((int)(Math.Round(255 * ambientColor[3])), (int)(Math.Round(255 * ambientColor[0])),
                                              (int)(Math.Round(255 * ambientColor[1])), (int)(Math.Round(255 * ambientColor[2])));
                return color;
            }
            set
            {
                if (ambientColor == null) { ambientColor = new float[] { 1f, 1f, 1f, 1f }; }
                ambientColor[0] = value.R/ (float)255;
                ambientColor[1] = value.G / (float)255;
                ambientColor[2] = value.B/ (float)255;
                ambientColor[3] = value.A / (float)255;
            }
        }

        [DataMember]
        public Color DiffuseColor
        {
            get
            {
                Color color = Color.FromArgb((int)(Math.Round(255 * diffuseColor[3])), (int)(Math.Round(255 * diffuseColor[0])),
                                              (int)(Math.Round(255 * diffuseColor[1])), (int)(Math.Round(255 * diffuseColor[2])));
                return color;
            }
            set
            {
                if (diffuseColor == null) { diffuseColor = new float[] { 1f, 1f, 1f, 1f }; }
                diffuseColor[0] = value.R / (float)255;
                diffuseColor[1] = value.G / (float)255;
                diffuseColor[2] = value.B / (float)255;
                diffuseColor[3] = value.A / (float)255;
            }
        }

        [DataMember]
        public Color SpecularColor
        {
            get
            {
                Color color = Color.FromArgb((int)(Math.Round(255 * specularColor[3])), (int)(Math.Round(255 * specularColor[0])),
                                              (int)(Math.Round(255 * specularColor[1])), (int)(Math.Round(255 * specularColor[2])));
                return color;
            }
            set
            {
                if (specularColor == null) { specularColor = new float[] { 1f, 1f, 1f, 1f }; }
                specularColor[0] = value.R / (float)255;
                specularColor[1] = value.G / (float)255;
                specularColor[2] = value.B / (float)255;
                specularColor[3] = value.A / (float)255;
            }
        }

        [DataMember]
        public int Shininess
        {
            get { return shininess; }
            set { shininess = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public float VertexSize
        {
            get { return vertexSize; }
            set { vertexSize = value; }
        }

        [DataMember]
        public float WireFrameWidth
        {
            get { return wireFrameWidth; }
            set { wireFrameWidth = value; }
        }

        [DataMember]
        public ShadingModel ShadingModel
        {
            get { return shadingModel; }
            set { shadingModel = value; }
        }

        [DataMember]
        public System.Boolean UseLight
        {
            get { return useLight; }
            set { useLight = value; }
        }

        [DataMember]
        public System.Boolean ShowVertices
        {
            get { return showVertices; }
            set { showVertices = value; }
        }

        [DataMember]
        public System.Boolean ShowWireFrame
        {
            get { return showWireFrame; }
            set { showWireFrame = value; }
        }

        [DataMember]
        public System.Boolean ShowSurfaces
        {
            get { return showSurfaces; }
            set { showSurfaces = value; }
        }

        [DataMember]
        public List<Object3D> Object3DList
        {
            get { return object3DList; }
            set { object3DList = value; }
        }

        public System.Boolean Visible
        {
            get { return visible; }
            set { visible = value; }
        }
    }
}
