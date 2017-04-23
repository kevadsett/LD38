using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
public class AmoebaMesh : MonoBehaviour {
	[SerializeField] int numverts;

	void Awake () {
		// abort if invalid shape
		if (numverts < 2) return;

		var verts = new Vector3[numverts + 1];
		verts[0] = new Vector3 (0f, 0f, 0f);

		var uvs = new Vector2[numverts + 1];
		uvs[0] = new Vector2 (0f, 0f);

		var tris = new int[numverts * 3];

		float offset = Random.Range (0f, Mathf.PI * 2f);

		// generate circle of verts
		float step = -(1f / numverts) * Mathf.PI * 2f;
		for (int i = 1; i <= numverts; i++) {
			float angle = i * step;
			float x = Mathf.Cos (angle + offset), y = Mathf.Sin (angle + offset);
			verts[i] = new Vector3 (x, y, 0f);
			uvs[i] = new Vector2 (1f, -angle);
		}

		// generate circle of tris
		int t = 0;
		for (int i = 1; i < numverts; i++) {
			tris[t++] = 0;
			tris[t++] = i;
			tris[t++] = i + 1;
		}

		// loop circle back around
		tris[t++] = 0;
		tris[t++] = numverts;
		tris[t++] = 1;

		// create & apply mesh object
		var mesh = new Mesh ();
		mesh.vertices = verts;
		mesh.uv = uvs;
		mesh.triangles = tris;
		GetComponent<MeshFilter> ().sharedMesh = mesh;
	}
}