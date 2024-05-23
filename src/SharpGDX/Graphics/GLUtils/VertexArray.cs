using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;
using Buffer = SharpGDX.Shims.Buffer;

namespace SharpGDX.Graphics.GLUtils
{
	/**
 * <p>
 * Convenience class for working with OpenGL vertex arrays. It interleaves all data in the order you specified in the constructor
 * via {@link VertexAttribute}.
 * </p>
 *
 * <p>
 * This class is not compatible with OpenGL 3+ core profiles. For this {@link VertexBufferObject}s are needed.
 * </p>
 *
 * @author mzechner, Dave Clayton <contact@redskyforge.com> */
public class VertexArray : IVertexData {
	readonly VertexAttributes attributes;
	readonly FloatBuffer buffer;
	readonly ByteBuffer byteBuffer;
	bool isBound = false;

	/** Constructs a new interleaved VertexArray
	 *
	 * @param numVertices the maximum number of vertices
	 * @param attributes the {@link VertexAttribute}s */
	public VertexArray (int numVertices, VertexAttribute[] attributes) 
	: this(numVertices, new VertexAttributes(attributes))
	{
		
	}

	/** Constructs a new interleaved VertexArray
	 *
	 * @param numVertices the maximum number of vertices
	 * @param attributes the {@link VertexAttributes} */
	public VertexArray (int numVertices, VertexAttributes attributes) {
		this.attributes = attributes;
		byteBuffer = BufferUtils.newUnsafeByteBuffer(this.attributes.vertexSize * numVertices);
		buffer = byteBuffer.asFloatBuffer();
		((Buffer)buffer).flip();
		((Buffer)byteBuffer).flip();
	}

	public void dispose () {
		BufferUtils.disposeUnsafeByteBuffer(byteBuffer);
	}
		
	public FloatBuffer getBuffer (bool forWriting) {
		return buffer;
	}

	public int getNumVertices () {
		return buffer.limit() * 4 / attributes.vertexSize;
	}

	public int getNumMaxVertices () {
		return byteBuffer.capacity() / attributes.vertexSize;
	}

	public void setVertices (float[] vertices, int offset, int count) {
		BufferUtils.copy(vertices, byteBuffer, count, offset);
		((Buffer)buffer).position(0);
		((Buffer)buffer).limit(count);
	}

	public void updateVertices (int targetOffset, float[] vertices, int sourceOffset, int count) {
		int pos = byteBuffer.position();
		((Buffer)byteBuffer).position(targetOffset * 4);
		BufferUtils.copy(vertices, sourceOffset, count, byteBuffer);
		((Buffer)byteBuffer).position(pos);
	}

	public void bind (ShaderProgram shader) {
		bind(shader, null);
	}

	public void bind (ShaderProgram shader, int[] locations) {
		int numAttributes = attributes.size();
		((Buffer)byteBuffer).limit(buffer.limit() * 4);
		if (locations == null) {
			for (int i = 0; i < numAttributes; i++) {
				 VertexAttribute attribute = attributes.get(i);
				 int location = shader.getAttributeLocation(attribute.alias);
				if (location < 0) continue;
				shader.enableVertexAttribute(location);

				if (attribute.type == GL20.GL_FLOAT) {
					((Buffer)buffer).position(attribute.offset / 4);
					shader.setVertexAttribute(location, attribute.numComponents, attribute.type, attribute.normalized,
						attributes.vertexSize, buffer);
				} else {
					((Buffer)byteBuffer).position(attribute.offset);
					shader.setVertexAttribute(location, attribute.numComponents, attribute.type, attribute.normalized,
						attributes.vertexSize, byteBuffer);
				}
			}
		} else {
			for (int i = 0; i < numAttributes; i++) {
				 VertexAttribute attribute = attributes.get(i);
				 int location = locations[i];
				if (location < 0) continue;
				shader.enableVertexAttribute(location);

				if (attribute.type == GL20.GL_FLOAT) {
					((Buffer)buffer).position(attribute.offset / 4);
					shader.setVertexAttribute(location, attribute.numComponents, attribute.type, attribute.normalized,
						attributes.vertexSize, buffer);
				} else {
					((Buffer)byteBuffer).position(attribute.offset);
					shader.setVertexAttribute(location, attribute.numComponents, attribute.type, attribute.normalized,
						attributes.vertexSize, byteBuffer);
				}
			}
		}
		isBound = true;
	}

	/** Unbinds this VertexBufferObject.
	 *
	 * @param shader the shader */
	public void unbind (ShaderProgram shader) {
		unbind(shader, null);
	}

	public void unbind (ShaderProgram shader, int[] locations) {
		 int numAttributes = attributes.size();
		if (locations == null) {
			for (int i = 0; i < numAttributes; i++) {
				shader.disableVertexAttribute(attributes.get(i).alias);
			}
		} else {
			for (int i = 0; i < numAttributes; i++) {
				 int location = locations[i];
				if (location >= 0) shader.disableVertexAttribute(location);
			}
		}
		isBound = false;
	}

	public VertexAttributes getAttributes () {
		return attributes;
	}

	public void invalidate () {
	}
}
}
