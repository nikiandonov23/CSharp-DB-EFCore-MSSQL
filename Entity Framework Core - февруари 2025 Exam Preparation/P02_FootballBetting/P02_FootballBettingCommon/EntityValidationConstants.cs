namespace P02_FootballBettingCommon;

public class EntityValidationConstants
{
    public static class Team  //този клас ще съдържа само константите за класа Team.С едни и същи имена са Team и Team.Nqma qdove
    {
        public const int TeamNameMaxLength = 75;
        public const int TeamUrlMaxLength = 2048;
        public const int TeamInitialsMaxLength = 5;

    }

    public static class Color
    {
        public const int ColorNameMaxLength = 30;
    }
    public static class Town
    {
        public const int TownNameMaxLength = 60;
    }

    public static class Country
    {
        public const int CountryNameMaxLength = 60;

    }

    public static class Player
    {
        public const int PlayerNameMaxLength = 100;

    }

    public static class Position
    {
        public const int PositionNameMaxLength = 30;

    }

    public static class Game
    {
        public const int GameResultMaxLength = 12;

    }

    public static class User
    {
        public const int UsernameMaxLength = 50;
        public const int NameMaxLength = 50;
        public const int PasswordMaxLength = 512;
        public const int EmailMaxLength = 50;




    }
}