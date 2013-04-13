namespace SearcherCore
{
	public interface IFileProcessor
	{
		bool IsSuitable(string fileName, string param);
	}
}
