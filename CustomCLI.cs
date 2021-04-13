using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSLectureDownloader
{
	public class CustomCLI
	{
		[Argument(0, Description = "EBS에서 다운로드 요청시 필요한 그룹 ID")]
		[Required]
		public int? GroupId { get; }
		[Option("-subtitle", Description = "자막 다운로드 여부")]
		public bool DownloadSubtitles { get; } = true;
		[Option("-folder", Description = "폴더 위치 여부")]
		public string Folder { get; } = Environment.GetEnvironmentVariable("EBSDOWN_FOLDER") ?? Directory.GetCurrentDirectory();

		private void OnExecute()
		{
			var client = new EbsClient();
			var xml = client.GetDownReservXml(GroupId.Value);
			var tasks = new List<Task>();
			foreach (var single in xml.Files)
				tasks.Add(client.DownloadFromReservFile(single, Folder, true));
			Console.WriteLine("다운로드를 시작합니다.");
			Task.WaitAll(tasks.ToArray());
			Console.WriteLine("다운로드가 종료되었습니다.");
		}
	}
}
