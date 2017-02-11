using System;
using System.Collections.Generic;
using Medallion.Shell;

namespace Testura.Android.PageObjectCreator.Util
{
    public class Terminal : ITerminal
    {
        /// <summary>
        /// Execute a new cmd command
        /// </summary>
        /// <param name="arguments">Arguments to send to the cmd</param>
        /// <returns>Output from cmd</returns>
        public string ExecuteCmdCommand(params string[] arguments)
        {
            var allArguments = new List<string> { "/c" };
            allArguments.AddRange(arguments);

            using (var command = Command.Run(
                "cmd.exe",
                allArguments,
                options: o => o.Timeout(TimeSpan.FromMinutes(1))))
            {
                var output = command.StandardOutput.ReadToEnd();
                var error = command.StandardError.ReadToEnd();

                if (!command.Result.Success)
                {
                    var message = $"Output: {output}, Error: {error}";
                    throw new Exception(message);
                }

                return output;
            }
        }
    }
}
