using UnityEngine;
using System.IO;
using System.Text;

// CSV 파일 읽어들이는 클래스
public class GameInfoReader
{
	public enum InfoType
	{
		Team,
		Map,
		Model,
		Attack,
	}

	delegate void AddDicHandler(string[] keys);
	private AddDicHandler AddDic;

	public void CSVFileOpen(InfoType infoType)
	{
		StringBuilder filePath = new StringBuilder();

		filePath.Append("CSV/");

		// Info Type에 맞는 추가 함수 및 경로 설정
		switch (infoType)
		{
			case InfoType.Team:
				AddDic = InfoManager.Instance.AddTeamDic;
				filePath.Append("PlayerDocument");
				break;

			case InfoType.Map:
				AddDic = InfoManager.Instance.AddMapDic;
				filePath.Append("MapDocument");
				break;

			case InfoType.Model:
				AddDic = InfoManager.Instance.AddModelDic;
				filePath.Append("ModelDocument");
				break;

			case InfoType.Attack:
				AddDic = InfoManager.Instance.AddAttackDic;
				filePath.Append("AttackDocument");
				break;

			default:
				Debug.Log("File open fail");
				break;
		}
		
		TextAsset fileText = Resources.Load<TextAsset>(filePath.ToString());

		StringReader sr = new StringReader(fileText.text);

		string line = sr.ReadLine(); // 첫째 라인은 안읽으므로 미리 한번 읽어줌

		while (line != null)
		{
			line = sr.ReadLine();

			if (string.IsNullOrEmpty(line))
			{
				break;
			}

			AddDic(line.Split(','));
		}
	}

	public void ReadGameInfo()
    {
		CSVFileOpen(InfoType.Team);
		CSVFileOpen(InfoType.Model);
		CSVFileOpen(InfoType.Attack);
		CSVFileOpen(InfoType.Map);

		InfoManager.Instance.MapID = 100; // 임시로 100 세팅

		Debug.Log("File Open Complete");
	}
}
