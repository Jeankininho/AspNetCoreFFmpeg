using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkySoftwareLibs.FFmpeg;

namespace AspNetCoreFFmpeg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FFmpegController : ControllerBase
    {

        private readonly ILogger<FFmpegController> _logger;

        public FFmpegController(ILogger<FFmpegController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public object Get()
        {
            // RODAR antes: apt-get install ffmpeg

            FFmpegConfig.SetDirectories("ffmpeg", "ffprobe", "/tmp/");

            var location = "input";

            var bytes = System.IO.File.ReadAllBytes(location);
            System.IO.File.WriteAllBytes("/tmp/input.mp4", bytes);

            var input = FFmpegProcess.GetInfo(location);

            return new { FFmpegResult = input, Locations = Directory.GetFiles("/tmp/") };

            //if (input.Streams.Any(p => p.codec_type.Equals("video")))
            //{
            //    var destinationthumb = Path.ChangeExtension(file, "jpg");
            //    FFmpegProcess.GetThumbNail(file, destinationthumb);
            //}
            //else if (input.Streams.Any(p => p.codec_type.Equals("audio")))
            //{
            //    Console.WriteLine($"Duration: {input.Format.durationTs}");
            //}
        }
    }
}
