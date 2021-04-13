using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EBSLectureDownloader
{
	public class EbsCLI
	{
		public void Run(string[] args)
		{
			var env = Environment.GetEnvironmentVariable("EBSDOWN_FOLDER");
			string folder;
			if (env != null)
			{
				folder = env;
				Console.WriteLine("EBSDOWN_FOLDER가 발견되었습니다.");
			} else
			{
				folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				Console.WriteLine("EBSDOWN_FOLDER가 발견되지 않았습니다. 다운로드될 폴더로 바탕화면으로 설정합니다.");
			}
			// parse to uri
			Uri uri;
			try
			{
				uri = new Uri(args[0]);
			} catch
			{
				throw new ArgumentException("요청된 주소가 올바르지 않거나 없습니다.");
			}
			// now parse parameters
			var queries = HttpUtility.ParseQueryString(uri.Query);

			var groupid = int.Parse(queries["groupID"]);
			var client = new EbsClient();
			var xml = client.GetDownReservXml(groupid);
			var tasks = new List<Task>();
			foreach (var single in xml.Files)
				tasks.Add(client.DownloadFromReservFile(single, folder, true));
			Console.WriteLine("다운로드를 시작합니다.");
			Task.WaitAll(tasks.ToArray());
			Console.WriteLine("다운로드가 종료되었습니다.");
		}
	}
}
