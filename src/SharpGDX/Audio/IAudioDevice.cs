using SharpGDX.Utils;

namespace SharpGDX.Audio
{
	/** Encapsulates an audio device in mono or stereo mode. Use the {@link #writeSamples(float[], int, int)} and
 * {@link #writeSamples(short[], int, int)} methods to write float or 16-bit signed short PCM data directly to the audio device.
 * Stereo samples are interleaved in the order left channel sample, right channel sample. The {@link #dispose()} method must be
 * called when this AudioDevice is no longer needed.
 *
 * @author badlogicgames@gmail.com */
	public interface IAudioDevice : Disposable
	{
		/// <summary>
		/// Returns whether this AudioDevice is in mono or stereo mode.
		/// </summary>
		/// <returns>Whether this AudioDevice is in mono or stereo mode.</returns>
		public bool IsMono();

		/** Writes the array of 16-bit signed PCM samples to the audio device and blocks until they have been processed.
		 *
		 * @param samples The samples.
		 * @param offset The offset into the samples array
		 * @param numSamples the number of samples to write to the device */
		public void WriteSamples(short[] samples, int offset, int numSamples);

		/** Writes the array of float PCM samples to the audio device and blocks until they have been processed.
		 *
		 * @param samples The samples.
		 * @param offset The offset into the samples array
		 * @param numSamples the number of samples to write to the device */
		public void WriteSamples(float[] samples, int offset, int numSamples);

		/// <summary>
		/// Returns the latency in samples.
		/// </summary>
		/// <returns>The latency in samples.</returns>
		public int GetLatency();

		/// <summary>
		/// Frees all resources associated with this AudioDevice.
		/// </summary>
		/// <remarks>
		/// Needs to be called when the device is no longer needed.
		/// </remarks>
		public new void dispose();

		/// <summary>
		/// Sets the volume in the range [0,1].
		/// </summary>
		/// <param name="volume">The volume.</param>
		public void SetVolume(float volume);

		/// <summary>
		/// Pauses the audio device if supported.
		/// </summary>
		public void Pause();

		/// <summary>
		/// Unpauses the audio device if supported.
		/// </summary>
		public void Resume();
	}
}