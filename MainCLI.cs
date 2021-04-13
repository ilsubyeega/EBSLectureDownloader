using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;

namespace EBSLectureDownloader
{
	class MainCLI
	{
		// 프로그램을 원하는 폴더에서 실행시키세요.
		// 만약 EBSDOWN_FOLDER 환경 변수가 주어진 경우, 해당 변수를 우선합니다.
		static void Main(string[] args)
		{
			if (args.Length > 0 && args[0].StartsWith(@"ebsdownloader2020://ebs.co.kr/callExe"))
			{
				new EbsCLI().Run(args);
			}
			else if (args.Length == 0)
			{
				Console.WriteLine("사용 방법: EBSLectureDownloader -h");
			}
			else
			{
				CommandLineApplication.Execute<CustomCLI>(args);
			}
			Console.ReadLine();
		}

		

	}
}
