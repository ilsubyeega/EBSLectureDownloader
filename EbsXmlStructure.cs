using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EBSLectureDownloader
{
	[XmlRoot("files")]
	public class DownReservXml
	{
		[XmlElement("file")]
		public List<DownReservFile> Files { get; set; }
	}

	public class DownReservFile
	{
		[XmlElement("name")]
		public string Name { get; set; }
		[XmlElement("callbackKey")] // 현재상 int로 표시됨, 어디에 사용되는지 의문
		public string CallbackKey { get; set; }
		[XmlElement("size")]
		public int Size { get; set; }
		[XmlElement("folder")]
		public string Folder { get; set; }
		[XmlElement("fileUrl")]
		public string FileUrl { get; set; }
		[XmlElement("captionUrl")]
		public string CaptionUrl { get; set; }
	}
}
