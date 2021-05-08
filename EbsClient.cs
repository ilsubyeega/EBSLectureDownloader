using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EBSLectureDownloader
{
	public class EbsClient
	{
		private string downReservXmlUrl = @"https://www.ebsi.co.kr/ebs/lms/downLoad/DownReservXmlNew.ebs?grpId=";
		/// <summary>
		/// ebs/lms/downLoad/DownReservXmlNew.ebs 에 호출해서 DownReservXml를 받아옵니다.
		/// </summary>
		/// <param name="groupId">호출된 groupId</param>
		/// <returns></returns>
		public DownReservXml GetDownReservXml(int groupId)
		{
			string url = downReservXmlUrl + groupId;
			Console.WriteLine($"{url}로 요청중..");
			using (var client = new WebClient())
			{
				client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
								  "Windows NT 5.2; .NET CLR 1.0.3705;)");
				var req = client.DownloadString(url);
				var serializer = new XmlSerializer(typeof(DownReservXml));
				using (var reader = new StringReader(req))
					return (DownReservXml)serializer.Deserialize(reader);
			}
		}
		private double byteToMegaByte(long bytes)
			=> Math.Round((double)(bytes / 1000 / 1000), 2);
		private string Combine(string folder, string foldername, string name, string url, bool addfolder = true)
			=> addfolder ? Path.Combine(folder, foldername, name + Path.GetExtension(url)) : Path.Combine(folder, name + Path.GetExtension(url));
		/// <summary>
		/// DownReservFile에서 파일들을 다운로드합니다.
		/// </summary>
		/// <param name="file">요청될 파일 오브젝트</param>
		/// <param name="folder">폴더 저장 위치</param>
		/// <param name="subtitles">자막 사용 여부</param>
		/// <param name="addfolder">폴더 사용 여부</param>
		/// <returns></returns>
		public async Task DownloadFromReservFile(DownReservFile file, string folder, bool subtitles, bool addfolder = true)
		{
			if (file == null || folder == null) throw new ArgumentNullException("파일이나 폴더가 null입니다.");
			using (var client = new WebClient())
			{
				var combined = Path.Combine(folder, file.Folder);
				if (addfolder && !Directory.Exists(combined))
					Directory.CreateDirectory(combined);

				// Download Subtitle
				await client.DownloadFileTaskAsync(new Uri(file.CaptionUrl), Combine(folder, file.Folder, file.Name, file.CaptionUrl, addfolder));

				client.DownloadProgressChanged += (sender, arg) =>
				{
					Console.WriteLine($"{file.Name} : {arg.ProgressPercentage}% | {byteToMegaByte(arg.BytesReceived)}MB/{byteToMegaByte(arg.TotalBytesToReceive)}MB");
				};

				// Download File
				await client.DownloadFileTaskAsync(new Uri(file.FileUrl), Combine(folder, file.Folder, file.Name, file.FileUrl));
			}
		}
	}
}
