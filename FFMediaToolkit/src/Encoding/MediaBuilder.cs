﻿namespace FFMediaToolkit.Encoding
{
    using System;
    using System.IO;
    using FFMediaToolkit.Common;

    /// <summary>
    /// Represents a multimedia file.
    /// </summary>
    public class MediaBuilder
    {
        private readonly MediaContainer container;
        private readonly string outputPath;
        private VideoEncoderSettings videoSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaBuilder"/> class.
        /// </summary>
        /// <param name="path">Path to create the output media file.</param>
        public MediaBuilder(string path)
        {
            if (!Path.IsPathFullyQualified(path))
                throw new ArgumentException($"The path \"{path}\" is not valid");

            container = MediaContainer.CreateOutput(path);
            outputPath = path;
        }

        /// <summary>
        /// Adds a new video stream to the file.
        /// </summary>
        /// <param name="settings">The video stream settings.</param>
        /// <returns>This <see cref="MediaBuilder"/> object.</returns>
        public MediaBuilder WithVideo(VideoEncoderSettings settings)
        {
            container.AddVideoStream(settings);
            videoSettings = settings;
            return this;
        }

        // TODO: Audio encoding

        /// <summary>
        /// Creates a multimedia file for specified video streams.
        /// </summary>
        /// <returns>A new <see cref="MediaOutput"/></returns>
        public MediaOutput Create()
        {
            container.LockFile(outputPath);

            return new MediaOutput(container, videoSettings);
        }
    }
}