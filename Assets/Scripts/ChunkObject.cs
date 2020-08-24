﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkObject : MonoBehaviour {

	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private Chunk chunk;

	public bool IsDirty { get; private set; }

	public void SetVoxel(Vector3Int blockIndex, Voxel voxel) {
		chunk[blockIndex] = voxel;
		IsDirty = true;
	}

	public void FillRange(Vector3Int corner1, Vector3Int corner2, Voxel voxel) {
		for(int z = corner1.z; z <= corner2.z; z++) {
			for (int y = corner1.y; y <= corner2.y; y++) {
				for (int x = corner1.x; x <= corner2.x; x++) {
					chunk[new Vector3Int(x, y, z)] = voxel;
				}
			}
		}

		IsDirty = true;
	}

	public void Initialise(Chunk chunkData) {
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();
		chunk = chunkData;
		IsDirty = true;

		if (meshFilter.sharedMesh == null)
			meshFilter.sharedMesh = new Mesh();

		if (meshRenderer.sharedMaterial == null)
			meshRenderer.sharedMaterial = new Material(Shader.Find("Shader Graphs/VoxelShader"));
	}

	public void UpdateMesh() {
		chunk.GenerateMesh().ApplyTo(meshFilter.sharedMesh);
		IsDirty = false;
	}
}