using Terraria;
using System;
using System.Collections.Generic;

namespace XritoMod
{
    public static class Upgrades
    {
        public static Dictionary<short, Upgrade> ID;

        public static List<Upgrade> unlockedUpgrades;
        public static List<Upgrade> boughtUpgrades;

        public static void InitUpgrades()
        {
            ID = new Dictionary<short, Upgrade>();
            short i = 0;
            ID.Add(i++, new Upgrade("Test Upgrade 1", "This is a test upgrade.", 0));
            ID.Add(i++, new Upgrade("Test Upgrade 2", "This is a test upgrade.", 0));
            ID.Add(i++, new Upgrade("Test Upgrade 3", "This is a test upgrade.", 0));
            ID.Add(i++, new Upgrade("Test Upgrade 4", "This is a test upgrade.", 0));
            ID.Add(i++, new Upgrade("Test Upgrade 5", "This is a test upgrade.", 0));
            //EMPTY UPGRADE:
            //ID.Add(i++, new Upgrade("", "", 0));
        }

        public static Upgrade GetUpgrade(int upgradeID)
        {
            Upgrade upgrade;
            ID.TryGetValue((short)upgradeID, out upgrade);
            return upgrade;
        }

        public static void UnlockUpgrade(int upgradeID)
        {
            Upgrade upgrade = GetUpgrade(upgradeID);
            unlockedUpgrades.Add(upgrade);
        }

        public static void BuyUpgrade(int upgradeID)
        {
            Upgrade upgrade = GetUpgrade(upgradeID);
            switch (upgrade.Name)
            {
                case "Test Upgrade 1":
                    Main.NewText("You actually won the game.");
                    break;
            }
            unlockedUpgrades.Remove(upgrade);
            boughtUpgrades.Add(upgrade);
        }
    }

    public class Upgrade
    {
        private String name;
        private String desc;
        private int cost;

        public String Name { get { return name; } }
        public String Desc { get { return desc; } }
        public int Cost { get { return cost; } }

        public Upgrade(String name, String desc, int cost)
        {
            this.name = name;
            this.desc = desc;
            this.cost = cost;
        }
    }
}