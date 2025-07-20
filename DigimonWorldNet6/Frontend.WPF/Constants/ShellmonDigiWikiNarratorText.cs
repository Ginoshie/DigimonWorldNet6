namespace DigimonWorld.Frontend.WPF.Constants;

public static class ShellmonDigiWikiNarratorText
{
    public static class HomeScreen
    {
        public const string IntroText = "Hey! Hello! I am Shellmon, the digimon famous for being a know-it-all!\n" +
                                        "\n" +
                                        "This the Digi-Wiki, a place where I get to share my knowledge with you.\n" +
                                        "\n" +
                                        "Click on any of the buttons to open the page of that topic.\n" +
                                        "\n" +
                                        "If you want even more information, after opening a topic, click any of the buttons in the \"More info\" to open the related online guide. If you want me to repeat the wiki text, just press the button in top right corner of this text section.\n" +
                                        "\n" +
                                        "Or try your luck clicking on some of the items on the page, who knows when and what I'll tell you. \n" +
                                        "\n" +
                                        "Happy browsing!";
    }

    public static class FoodWiki
    {
        public const string WikiText = "Food gives your Digimon a set amount of energy and increase or decrease in weight.  Some foods also reduce Tiredness, increase Happiness and/or Discipline as well as certain stats.\n" +
                                       "\n" +
                                       "Over time and when performing actions energy will go down.  A digimon becomes hungry at fixed times, indepedent of it's energy level.  When your Digimon gets hungry the starvation timer starts." +
                                       "Either feed it until the energy storage hits a certain threshold or wait for the timer to run out to stop it's hunger.\n" +
                                       "\n" +
                                       "If the starvation timer runs out while the energy value is below threshold then it'll result in a care mistake.\n" +
                                       "\n" +
                                       "Click on the images below if you want me to tell you more about them.";

        public const string ShellFacts = "Shell Fact One\n" +
                                         "Each digimon has a favorite food which gives them 40% more energy and +2 Happiness!\n" +
                                         "\n" +
                                         "Shell Fact Two\n" +
                                         "When your Digimons energy level reaches 0 it loses 1 weight every 10 minutes in-game.\n" +
                                         "\n" +
                                         "Shell Fact Three\n" +
                                         "Training increases energy consumption by 1.";

        public const string HungerConditionOverworld = "When hungry, your digimon will have a emotion bubble with a meat icon above its head. As long as this emotion bubble is visible it is hungry.\n" +
                                                       "\n" +
                                                       "It will make a uncomfortable movements and growl.\n" +
                                                       "\n" +
                                                       "Doesn't it make you want to feed it?";

        public const string HungerConditionScreen = "When hungry, your digimon will have a status modifier in the CONDITION section of the Status tab of the Digimon menu.\n" +
                                                    "\n" +
                                                    "To get to this screen, open the main menu by pressing triangle. Then open the Digimon menu. It opens up at the Status tab, in the bottom left corner you'll find the CONDITION section.";
    }

    public static class WeightWiki
    {
        public const string WikiText = "The weight of your digimon is a very dynamic parameter as it is influenced by many events.\n" +
                                       "\n" +
                                       "The following events influence the weight of your digimon:\n" +
                                       "\n" +
                                       "- Feeding: Depending on the food it causes a reduction or increase.\n" +
                                       "- Sleeping: Reduction of 1/10th of the default weight.\n" +
                                       "- Pooping: Reduction 1/4th of the poop size + a random value between 0 and 3.\n" +
                                       "- 0 Energy: Reduction every 10 minutes while having 0 energy your digimon will lose 1 weight.";

        public const string ShellFacts = "Shell Fact One\n" +
                                         "Default weight can be found in the datasheet \"Digimon Raise\".\n" +
                                         "\n" +
                                         "Shell Fact Two\n" +
                                         "Time passed through training will not induce the weight reduction from 0 energy.";
    }
}