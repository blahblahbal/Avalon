using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace Avalon.Common;
public static class StripRenderer
{
	public struct StripVertexData : IVertexType
	{
		public Vector2 Position;
		public Color Color;
		public Vector3 TextureCoordinate;

		public StripVertexData(Vector2 position, Color color, Vector3 textureCoordinate)
		{
			Position = position;
			Color = color;
			TextureCoordinate = textureCoordinate;
		}

		public VertexDeclaration VertexDeclaration => declaration;

		private static readonly VertexDeclaration declaration = new(
			new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
			new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
		);
	}

	public delegate Color StripColorFunction(float progressAlongStrip);

	public delegate float StripWidthFunction(float progressAlongStrip);

	public static void DrawStrip(Vector2[] positions, float[] rotations, StripColorFunction color, StripWidthFunction width, Vector2 offset = default)
	{
		if (positions.Length < 2 | positions.Length != rotations.Length)
		{
			return;
		}

		var length = positions.Length;
		var vertices = new StripVertexData[length * 2];
		var indices = new short[length * 6];

		for (var i = 0; i < length; i++)
		{
			SetVertexPair(ref vertices, positions[i] + offset, rotations[i], color, width, i * 2);
		}

		SetIndices(ref indices, vertices);

		Main.instance.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
		Main.instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
	}

	public static void DrawStripPadded(Vector2[] positions, float[] rotations, StripColorFunction color, StripWidthFunction width, Vector2 offset = default)
	{
		var filteredPositions = new List<Vector2>();
		var filteredRotations = new List<float>();

		for (var i = 0; i < positions.Length; i++)
		{
			if (positions[i] == Vector2.Zero)
			{
				continue;
			}

			filteredPositions.Add(positions[i]);
			filteredRotations.Add(rotations[i]);
		}

		if (filteredPositions.Count < 2)
		{
			return;
		}

		var meshPositions = new List<Vector2>();
		var meshRotations = new List<float>();

		for (var i = 0; i < filteredPositions.Count - 1; i++)
		{
			var p0 = filteredPositions[i];
			var p1 = filteredPositions[i + 1];

			var r0 = MathHelper.WrapAngle(filteredRotations[i]);
			var r1 = MathHelper.WrapAngle(filteredRotations[i + 1]);

			var rDelta = MathHelper.WrapAngle(r1 - r0);
			var dist = Vector2.Distance(p0, p1);

			// Create subdivisions for sharp angles and long segments.
			var subdivisions = 1 + (int)(Math.Abs(rDelta) / (MathHelper.Pi / 16f)) + (int)(dist / 12f);

			for (var s = 0; s < subdivisions; s++)
			{
				var amt = (float)s / subdivisions;

				var pos = Vector2.CatmullRom(
					filteredPositions[Math.Max(0, i - 1)],
					p0,
					p1,
					filteredPositions[Math.Min(filteredPositions.Count - 1, i + 2)],
					amt
				);
				meshPositions.Add(pos);

				var rot = r0 + rDelta * amt;
				meshRotations.Add(rot);
			}
		}

		meshPositions.Add(filteredPositions[^1]);
		meshRotations.Add(MathHelper.WrapAngle(filteredRotations[^1]));

		var length = meshPositions.Count;
		var vertices = new StripVertexData[length * 2];
		var indices = new short[(length - 1) * 6];

		for (var i = 0; i < length; i++)
		{
			SetVertexPair(ref vertices, meshPositions[i] + offset, meshRotations[i], color, width, i * 2);
		}

		SetIndices(ref indices, vertices);

		Main.instance.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
		Main.instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
	}

	private static void SetVertexPair(ref StripVertexData[] vertices, Vector2 position, float rotation, StripColorFunction colorFunction, StripWidthFunction widthFunction, int i)
	{
		var progress = (float)i / (vertices.Length - 1);
		var width = widthFunction(progress);
		var color = colorFunction(progress);
		var unitRotation = MathHelper.WrapAngle(rotation + MathHelper.PiOver2).ToRotationVector2();
		var offset = unitRotation * width;

		vertices[i].Position = position - offset;
		vertices[i].TextureCoordinate = new Vector3(progress, 0.5f - 0.5f * width, width);
		vertices[i].Color = color;
		vertices[i + 1].Position = position + offset;
		vertices[i + 1].TextureCoordinate = new Vector3(progress, 0.5f + 0.5f * width, width);
		vertices[i + 1].Color = color;
	}

	private static void SetIndices(ref short[] indices, in StripVertexData[] vertices)
	{
		short iI = 0;
		for (short i = 0; i < vertices.Length / 2 - 1; i++)
		{
			var nextIndex = i * 2;
			indices[iI++] = (short)nextIndex;
			indices[iI++] = (short)(nextIndex + 1);
			indices[iI++] = (short)(nextIndex + 2);
			indices[iI++] = (short)(nextIndex + 2);
			indices[iI++] = (short)(nextIndex + 3);
			indices[iI++] = (short)(nextIndex + 1);
		}
	}
}

