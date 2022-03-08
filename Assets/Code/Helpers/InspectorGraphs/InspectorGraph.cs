using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Helpers.InspectorGraphs {
	public class InspectorGraph {
		static readonly int srcBlend = Shader.PropertyToID("_SrcBlend");
		static readonly int dstBlend = Shader.PropertyToID("_DstBlend");
		static readonly int cull = Shader.PropertyToID("_Cull");
		static readonly int zWrite = Shader.PropertyToID("_ZWrite");

		readonly Material lineMaterial;

		public InspectorGraph() {
			var shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new(shader) {
				hideFlags = HideFlags.HideAndDontSave
			};
			lineMaterial.SetInt(srcBlend, (int) BlendMode.SrcAlpha);
			lineMaterial.SetInt(dstBlend, (int) BlendMode.OneMinusSrcAlpha);
			lineMaterial.SetInt(cull, (int) CullMode.Off);
			lineMaterial.SetInt(zWrite, 0);
		}

		public void renderGraph(List<float> data, float xScale, float yScale) {
			var rect = GUILayoutUtility.GetRect(10, 1000, 200, 200);

			GUI.BeginClip(rect);
			GL.PushMatrix();
			GL.Clear(true, false, Color.black);
			lineMaterial.SetPass(0);

			GL.Begin(GL.LINES);
			GL.Color(Color.green);
			for (var i = 0; i < data.Count - 1; ++i) {
				setGLVector(i);
				setGLVector(i + 1);
			}
			GL.End();
			GUI.EndClip();

			void setGLVector(int i) => GL.Vertex3(i * xScale, data[i] * yScale, 0);
		}
	}
}