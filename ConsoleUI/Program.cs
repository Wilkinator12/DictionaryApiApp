using ConsoleLibrary.Common.Extensions;
using ConsoleLibrary.Prompts.CommonPrompts.Extensions;
using ConsoleUI.Configuration;
using DictionaryLibrary.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI
{
    internal class Program
    {
        private static readonly IServiceProvider _serviceProvider = AppConfiguration.Instance.ServiceProvider;


        static async Task Main(string[] args)
        {
            "Word Lookup App!".PrintAsTitle();



            bool continueLooping = true;

            while (continueLooping == true)
            {
                await PromptWordLookup();
                Console.WriteLine();


                string yesNoPromptMessage = "Would you like to lookup another word?";

                continueLooping = yesNoPromptMessage.AsYesNoPrompt();
                ConsolePrinter.PrintLine('-', yesNoPromptMessage.Length);
                Console.WriteLine();
            }



            Console.WriteLine("End of Program.");
            Console.ReadLine();
        }

        private static async Task PromptWordLookup()
        {
            var db = _serviceProvider.GetService<IDictionaryApiDataAccess>();


            if (db == null) throw new NullReferenceException($"Could not find the service: {nameof(db)}");


            var word = "Please enter a word to get the definition".AsStringPrompt();

            try
            {
                var lookupTask = db.LookupWord(word);

                Console.WriteLine("Loading...");

                var definition = (await lookupTask).First();
                Console.WriteLine($"Definition of {definition.Word}: {definition.Meanings.First().Definitions.First().Definition}");
            }
            catch
            {
                Console.WriteLine($"No definition for {word} was found.");
            }
        }
    }
}
