using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectYZ
{

    [XmlRoot(ElementName = "Bot")]
    public class Bot
    {
        [Required]
        [XmlAttribute(AttributeName = "PcId")]
        public string PcId { get; set; }
    }

    [XmlRoot(ElementName = "Telegram")]
    public class Telegram
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 40, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "ApiKey")]
        public string ApiKey { get; set; }

        [Required]
        [Range(1, 1000000000000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "UserId")]
        public int UserId { get; set; }
    }

    [XmlRoot(ElementName = "Backward")]
    public class Backward
    {
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "StopAutoMovement")]
    public class StopAutoMovement
    {
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "StartMovement")]
    public class StartMovement
    {
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "TurnLeft")]
    public class TurnLeft
    {
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "TurnRight")]
    public class TurnRight
    {
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "Jump")]
    public class Jump
    {
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 1, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "FoodInfo")]
    public class FoodInfo
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "WaterInfo")]
    public class WaterInfo
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "LastProfile")]
    public class LastProfile
    {
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Path")]
        public string Path { get; set; }
    }

    [XmlRoot(ElementName = "LastNav")]
    public class LastNav
    {
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "Path")]
        public string Path { get; set; }
    }

    [XmlRoot(ElementName = "Login")]
    public class Login
    {
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "login")]
        public string LoginString { get; set; }
    }

    [XmlRoot(ElementName = "Password")]
    public class Password
    {
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "password")]
        public string PasswordString { get; set; }
    }

    [XmlRoot(ElementName = "LastX")]
    public class LastX
    {
        [Required]
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "lastX")]
        public string lastX { get; set; }
    }

    [XmlRoot(ElementName = "LastY")]
    public class LastY
    {
        [Required]
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Invalid string!")]
        [XmlAttribute(AttributeName = "lastY")]
        public string lastY { get; set; }
    }

    [XmlRoot(ElementName = "OneSessionTime")]
    public class OneSessionTime
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "time")]
        public int oneSessionTime { get; set; } 
    }

    [XmlRoot(ElementName = "BetweenSessionTime")]
    public class BetweenSessionTime
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "time")]
        public int betweenSessionTime { get; set; }
    }

    [XmlRoot(ElementName = "Commonsettings")]
    public class Commonsettings
    {
        [Required, ValidateObject]
        [XmlElement(ElementName = "Bot")]
        public Bot Bot { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Telegram")]
        public Telegram Telegram { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Backward")]
        public Backward Backward { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "StopAutoMovement")]
        public StopAutoMovement StopAutoMovement { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "StartMovement")]
        public StartMovement StartMovement { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TurnLeft")]
        public TurnLeft TurnLeft { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TurnRight")]
        public TurnRight TurnRight { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Jump")]
        public Jump Jump { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "LastProfile")]
        public LastProfile LastProfile { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "LastNav")]
        public LastNav LastNav { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "Login")]
        public Login Login { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "Password")]
        public Password Password { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "LastX")]
        public LastX LastX { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "LastY")]
        public LastY LastY { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "OneSessionTime")]
        public OneSessionTime OneSessionTime { get; set; }

        [ValidateObject]
        [XmlElement(ElementName = "BetweenSessionTime")]
        public BetweenSessionTime betweenSessionTime { get; set; }
    }

    [XmlRoot(ElementName = "X")]
    public class X
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Y")]
    public class Y
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Angle")]
    public class Angle
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Hp")]
    public class Hp
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Mp")]
    public class Mp
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "TargetHp")]
    public class TargetHp
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Aggro")]
    public class Aggro
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Invalid string!")]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Targetdistance")]
    public class Targetdistance
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Targetdead")]
    public class Targetdead
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Targetgrey")]
    public class Targetgrey
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Died")]
    public class Died
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Mounted")]
    public class Mounted
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Repair")]
    public class Repair
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Bag")]
    public class Bag
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Whishper")]
    public class Whishper
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Spellstatus")]
    public class Spellstatus
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Needtoface")]
    public class Needtoface
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Mobid")]
    public class Mobid
    {
        [Required]
        [Range(1, 200000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Lootready")]
    public class Lootready
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "FailedAttempt")]
    public class FailedAttempt
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Moneycount")]
    public class Moneycount
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }
    }

    [XmlRoot(ElementName = "Isingame")]
    public class Isingame
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Invalid number")]
        [XmlAttribute(AttributeName = "position")]
        public int Position { get; set; }

        [Required]
        [RegularExpression(@"[0-9a-zA-Z]{6}", ErrorMessage = "Characters are not allowed.")]
        [XmlAttribute(AttributeName = "colorHex")]
        public string ColorHex { get; set; }
    }

    [XmlRoot(ElementName = "Pixelsettings")]
    public class Pixelsettings
    {
        [Required, ValidateObject]
        [XmlElement(ElementName = "X")]
        public X X { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Y")]
        public Y Y { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Angle")]
        public Angle Angle { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Hp")]
        public Hp Hp { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Mp")]
        public Mp Mp { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TargetHp")]
        public TargetHp TargetHp { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Aggro")]
        public Aggro Aggro { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Targetdistance")]
        public Targetdistance Targetdistance { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Targetdead")]
        public Targetdead Targetdead { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Targetgrey")]
        public Targetgrey Targetgrey { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Died")]
        public Died Died { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Mounted")]
        public Mounted Mounted { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Repair")]
        public Repair Repair { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Bag")]
        public Bag Bag { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Whishper")]
        public Whishper Whishper { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "FoodInfo")]
        public FoodInfo Food { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "WaterInfo")]
        public WaterInfo Water { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Spellstatus")]
        public Spellstatus Spellstatus { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Needtoface")]
        public Needtoface Needtoface { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Mobid")]
        public Mobid Mobid { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Lootready")]
        public Lootready Lootready { get; set; }

        [Required]
        [XmlElement(ElementName = "Moneycount")]
        public Moneycount Moneycount { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Isingame")]
        public Isingame Isingame { get; set; }

        [XmlElement(ElementName = "FailedAttempt")]
        [Required, ValidateObject]
        public FailedAttempt FailedAttempt { get; set; }

        [XmlAttribute(AttributeName = "start")]
        [Required]
        [Range(1, 100, ErrorMessage = "Invalid number")]
        public int Start { get; set; }

        [XmlAttribute(AttributeName = "height")]
        [Required]
        [Range(1, 100, ErrorMessage = "Invalid number")]
        public int Height { get; set; }

        [XmlAttribute(AttributeName = "size")]
        [Required]
        [Range(1, 100, ErrorMessage = "Invalid number")]
        public int Size { get; set; }
    }

    [XmlRoot(ElementName = "config")]
    public class Config
    {
        [XmlElement(ElementName = "Commonsettings")]
        [Required, ValidateObject]
        public Commonsettings Commonsettings { get; set; }

        [XmlElement(ElementName = "Pixelsettings")]
        [Required, ValidateObject]
        public Pixelsettings Pixelsettings { get; set; }
    }

    class ConfigSettings
    {
        public static Config Cfg = new Config();
        public static string ConfigLocation = "config.xml";
        public static string DefaultConfigLocation = "DefaultCfg.xml";

        public static bool ConfigReader()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (FileStream reader = new FileStream(ConfigLocation, FileMode.OpenOrCreate))
                {
                    Cfg = (Config)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Debug.AddDebugRecord("Config: " + e.Message, true);
                return false;
            }

            List<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(Cfg, new ValidationContext(Cfg), results, true))
            {
                ValidateObjectAttribute.PrintResults(ConfigLocation + " : ", results);
                return false;
            }

            return true;
        }

        public static bool ConfigInitDefault()
        {
            if (!File.Exists(DefaultConfigLocation))
                File.Copy(DefaultConfigLocation, ConfigLocation);
            else return false;
            return true;
        }

        public static void ConfigWriter()
        {
            if (File.Exists(ConfigLocation)) File.Delete(ConfigLocation);
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream fs = new FileStream(ConfigLocation, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, Cfg);
            }
        }
    }
}