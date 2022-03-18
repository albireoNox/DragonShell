using System.Collections.Generic;
using System.IO;
using game_system_rulesets.objects;

namespace main_cli.io.file
{
    public static class RulesetIo
    {
        private static readonly string RULESET_DIR = "ruleset_data";
        private static readonly string ENTITIES_DIR = "entities";
        private static readonly string RULESET_MANIFEST_NAME = "system.yaml";

        public static Ruleset loadRuleset(string gameSystemId)
        {
            DirectoryInfo curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo rulesetRootDir = getSubdirectory(curDir, RULESET_DIR);

            foreach (var rulesetDir in rulesetRootDir.GetDirectories())
            {
                // Search for system file
                FileInfo manifestFile = getFile(rulesetDir, RULESET_MANIFEST_NAME);

                RulesetInfo rulesetInfo = YamlFile.deserializeYamlFile<RulesetInfo>(manifestFile);
                if (rulesetInfo.Id.Equals(gameSystemId))
                {
                    return buildRuleset(rulesetInfo, rulesetDir);
                }
            }

            throw new IOException($"Could not find rule set for game system '{gameSystemId}'");
        }

        private static DirectoryInfo getSubdirectory(DirectoryInfo dir, string subdirName)
        {
            var subdirs = dir.GetDirectories(subdirName);
            if (subdirs.Length == 0)
            {
                throw new IOException($"Could not find directory '{subdirName}' in '{dir.FullName}'");
            } 
            else if (subdirs.Length > 1)
            {
                throw new IOException($"Ambiguous name '{subdirName}' in '{dir.FullName}'");
            }

            return subdirs[0];
        }

        private static FileInfo getFile(DirectoryInfo dir, string fileName)
        {
            var files = dir.GetFiles(fileName);
            if (files.Length == 0)
            {
                throw new IOException($"Could not find file '{fileName}' in '{dir.FullName}'");
            }
            else if (files.Length > 1)
            {
                throw new IOException($"Ambiguous name '{fileName}' in '{dir.FullName}'");
            }

            return files[0];
        }

        private static Ruleset buildRuleset(RulesetInfo info, DirectoryInfo root)
        {
            DirectoryInfo entityDir = getSubdirectory(root, ENTITIES_DIR);

            List<EntityType> entities = new List<EntityType>();
            foreach (var entityDefFile in entityDir.EnumerateFiles("*.yaml"))
            {
                entities.Add(YamlFile.deserializeYamlFile<EntityType>(entityDefFile));
            }

            return new Ruleset(info, entities);
        }
    }
}
