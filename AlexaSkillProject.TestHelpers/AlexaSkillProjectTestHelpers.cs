using AlexaSkillProject.Domain;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AlexaSkillProject.TestHelpers
{
    public static class AlexaSkillProjectTestHelpers
    {

        public static AlexaRequestPayload GetAlexaRequestPayload(string filename)
        {
            var dir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName);
            var path = string.Format("{0}\\{1}", dir, filename);

            AlexaRequestPayload alexaRequestPayload = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                alexaRequestPayload = (AlexaRequestPayload)serializer.Deserialize(file, typeof(AlexaRequestPayload));
            }

            return alexaRequestPayload;
        }

    }
    
}
