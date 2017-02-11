namespace Testura.Android.PageObjectCreator.Util
{
    public interface ITerminal
    {
        /// <summary>
        /// Execute a new cmd command
        /// </summary>
        /// <param name="arguments">Arguments to send to the cmd</param>
        /// <returns>Output from cmd</returns>
        string ExecuteCmdCommand(params string[] arguments);
    }
}
