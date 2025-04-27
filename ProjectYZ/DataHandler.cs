using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectYZ
{
    [XmlRoot(ElementName = "Dot")]
    public class Dot
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Dot name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong Dot X Range!")]
        [XmlAttribute(AttributeName = "X")]
        public double X { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong Dot Y Range!")]
        [XmlAttribute(AttributeName = "Y")]
        public double Y { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong Dot Radius Range!")]
        [XmlAttribute(AttributeName = "Radius")]
        public double Radius { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong Dot Accuracy Range!")]
        [XmlAttribute(AttributeName = "Accuracy")]
        public double Accuracy { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Wrong Dot ScreenShotTimeout Range!")]
        [XmlAttribute(AttributeName = "ScreenshotTimeout")]
        public int ScreenshotTimeout { get; set; }
        [XmlIgnore]
        public bool Mount { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "Dot mount error!")]
        [XmlAttribute(AttributeName = "Mount")]
        public string MountL
        {
            set => Mount = Convert.ToBoolean(value);
            get => Mount.ToString();
        }
        public Dot() { }

        public Dot(double x, double y, string name = "", double radius = 0.2, double accuracy = 0.1, int screenshottimeout = 1000, bool mount = false)
        {
            X = x;
            Y = y;
            Name = name;
            Radius = radius;
            Accuracy = accuracy;
            ScreenshotTimeout = screenshottimeout;
            Mount = mount;
        }
    }

    [XmlRoot(ElementName = "SpotDots")]
    public class SpotDots
    {
        [XmlElement("Dot")]
        public List<Dot> Dot = new List<Dot>();
    }

    [XmlRoot(ElementName = "VendorDots")]
    public class VendorDots
    {
        [XmlElement("Dot")]
        public List<Dot> Dot = new List<Dot>();
    }

    [XmlRoot(ElementName = "GhostDots")]
    public class GhostDots
    {
        [XmlElement("Dot")]
        public List<Dot> Dot = new List<Dot>();
    }

    [XmlRoot(ElementName = "Edge")]
    public class Edge
    {
        [Required]
        [XmlAttribute(AttributeName = "Dot1")]
        public string Dot1 { get; set; }

        [Required]
        [XmlAttribute(AttributeName = "Dot2")]
        public string Dot2 { get; set; }

        public Edge() { }
        public Edge(string p1, string p2)
        {
            Dot1 = p1;
            Dot2 = p2;
        }
    }

    [XmlRoot(ElementName = "EdgeList")]
    public class EdgeList
    {
        [XmlElement("Edge")]
        public List<Edge> Edge = new List<Edge>();
    }

    [XmlRoot(ElementName = "DotsList")]
    public class DotsList
    {
        [Required]
        [XmlElement(ElementName = "SpotDots")]
        public SpotDots SpotDots { get; set; }
        [Required]
        [XmlElement(ElementName = "VendorDots")]
        public VendorDots VendorDots { get; set; }
        [Required]
        [XmlElement(ElementName = "GhostDots")]
        public GhostDots GhostDots { get; set; }
        [Required]
        [XmlElement(ElementName = "EdgeList")]
        public EdgeList EdgeList { get; set; }
    }
    public class MeshHandler
    {
        public static string MeshPath { get; set; }
        public static DotsList MeshDots = new DotsList();

        public static bool ReadMesh()
        {
            Console.WriteLine(MeshPath);
            if (!File.Exists(MeshPath))
            {
                MessageBox.Show("No meshes file!!!\n");
                return false;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DotsList));
                using (FileStream reader = new FileStream(MeshPath, FileMode.OpenOrCreate))
                {
                    MeshDots = (DotsList)serializer.Deserialize(reader);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error raised when read meshes!!!\n" + e.Message.ToString());
                return false;
            }

            List<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(MeshDots, new ValidationContext(MeshDots), results, true))
            {
                ValidateObjectAttribute.PrintResults(MeshPath + " : ", results);
                return false;
            }

            foreach (Dot d in MeshDots.SpotDots.Dot.Concat(MeshDots.VendorDots.Dot).Concat(MeshDots.GhostDots.Dot))
            {
                Validator.TryValidateObject(d, new ValidationContext(d), results, true);
            }

            foreach (Edge edg in MeshDots.EdgeList.Edge)
            {
                Validator.TryValidateObject(edg, new ValidationContext(edg), results, true);
            }

            if (results.Count > 0)
            {
                ValidateObjectAttribute.PrintResults(MeshPath + " : ", results);
                MessageBox.Show("Errors when checked file validity!");
                return false;
            }

            if (FindDotNameDuplicates(MeshDots.SpotDots.Dot.Concat(MeshDots.GhostDots.Dot).Concat(MeshDots.VendorDots.Dot)).ToList().Count > 0)
            {
                MessageBox.Show("Duplicates names found!");
                return false;
            }

            if (FindDotCoordDuplicates(MeshDots.SpotDots.Dot.Concat(MeshDots.GhostDots.Dot).Concat(MeshDots.VendorDots.Dot)).ToList().Count > 0)
            {
                MessageBox.Show("Duplicates coords found!");
                return false;
            }

            return true;
        }


        public static Dot GetPointByName(string name) => MeshDots.SpotDots.Dot.Concat(MeshDots.GhostDots.Dot).Concat(MeshDots.VendorDots.Dot).First(item => item.Name == name);
        public static List<Dot> MergedSpotVendor() => MeshDots.SpotDots.Dot.Concat(MeshDots.VendorDots.Dot).ToList();
        public static List<Dot> MergedSpotGhost() => MeshDots.SpotDots.Dot.Concat(MeshDots.GhostDots.Dot).ToList();
        public static List<Dot> MergedSpotVendorGhost() => MeshDots.SpotDots.Dot.Concat(MeshDots.VendorDots.Dot).Concat(MeshDots.GhostDots.Dot).ToList();
        public static List<Dot> MergedVendorGhost() => MeshDots.VendorDots.Dot.Concat(MeshDots.GhostDots.Dot).ToList();

        public static void AppendDot(string type, string name, double x, double y, double r, double accuracy, int scr, bool mount)
        {
            switch (type)
            {
                case "spot": MeshDots.SpotDots.Dot.Add(new Dot(x, y, name, r, accuracy, scr, mount)); break;
                case "vendor": MeshDots.VendorDots.Dot.Add(new Dot(x, y, name, r, accuracy, scr, mount)); break;
                case "ghost": MeshDots.GhostDots.Dot.Add(new Dot(x, y, name, r, accuracy, scr, mount)); break;
                default: break;
            }
        }

        public static void AppendDotAndConnectWithNearestExist(string type, string name, double x, double y, double r = 0.2, double accuracy = 1, int scr = 300, bool mount = false)
        {
            Dot nearPoint = Navigation.GetNearestPoint(MergedSpotVendorGhost(), new Dot(x, y));
            switch (type)
            {
                case "spot": MeshDots.SpotDots.Dot.Add(new Dot(x, y, name, r, accuracy, scr, mount)); break;
                case "vendor": MeshDots.VendorDots.Dot.Add(new Dot(x, y, name, r, accuracy, scr, mount)); break;
                case "ghost": MeshDots.GhostDots.Dot.Add(new Dot(x, y, name, r, accuracy, scr, mount)); break;
                default: break;
            }
            Console.WriteLine(string.Format("Edge: {0} - {1}", name, nearPoint.Name));
            AppendEdge(name, nearPoint.Name);
        }
        public static IEnumerable<double> FindDotCoordDuplicates(IEnumerable<Dot> l) => l.Select(k => k.X).ToList().GroupBy(x => x).Where(g => g.Count() > 1).Select(x => x.Key);

        public static IEnumerable<string> FindDotNameDuplicates(IEnumerable<Dot> l) => l.Select(k => k.Name).ToList().GroupBy(x => x).Where(g => g.Count() > 1).Select(x => x.Key);

        public static void DeleteEdge(string p1, string p2) => MeshDots.EdgeList.Edge.RemoveAll(item => (item.Dot1 == p1 || item.Dot1 == p2) && (item.Dot2 == p1 || item.Dot2 == p2));

        public static void DeleteEdgesConnectedToDot(string p1) => MeshDots.EdgeList.Edge.RemoveAll(item => item.Dot1 == p1 || item.Dot2 == p1);

        public static bool DeleteDot(string p1) => MeshDots.SpotDots.Dot.RemoveAll(x => x.Name == p1) != 0 | MeshDots.VendorDots.Dot.RemoveAll(x => x.Name == p1) != 0 | MeshDots.GhostDots.Dot.RemoveAll(x => x.Name == p1) != 0;

        public static void AppendEdge(string p1, string p2) => MeshDots.EdgeList.Edge.Add(new Edge(p1, p2));

        public static bool PointCoordsAvailability(double x, double y) => MeshDots.SpotDots.Dot.Concat(MeshDots.GhostDots.Dot).Concat(MeshDots.VendorDots.Dot).Select(item => item.X == x && item.Y == y).ToList().Contains(true);

        public static bool PointNameAvailability(string name) => MeshDots.SpotDots.Dot.Concat(MeshDots.GhostDots.Dot).Concat(MeshDots.VendorDots.Dot).Select(item => item.Name).ToList().Contains(name);

        public static bool EdgeAvailability(string p1, string p2) => MeshDots.EdgeList.Edge.Select(item => (item.Dot1 == p1 || item.Dot1 == p2) && (item.Dot2 == p1 || item.Dot2 == p2)).ToList().Contains(true);

        public static void WriteMesh()
        {
            if (File.Exists(MeshPath)) File.Delete(MeshPath);
            XmlSerializer serializer = new XmlSerializer(typeof(DotsList));
            using (FileStream fs = new FileStream(MeshPath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, MeshDots);
            }
        }
    }

    [XmlRoot(ElementName = "MainDot")]
    public class MainDot
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        public MainDot() { }
    }

    [XmlRoot(ElementName = "MainDots")]
    public class MainDots
    {
        [XmlElement(ElementName = "MainDot")]
        public List<MainDot> MainDot = new List<MainDot>();
    }

    [XmlRoot(ElementName = "Buff")]
    public class Buff
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }

        [Required]
        [Range(0, 30000, ErrorMessage = "Wrong castdelay!")]
        [XmlAttribute(AttributeName = "CastDelay")]
        public int CastDelay { get; set; }

        [XmlIgnore]
        public DateTime lastUse = DateTime.Now.AddHours(-1);

        [XmlIgnore]
        public bool BeforeCombat { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "BeforeCombat bool error!")]
        [XmlAttribute(AttributeName = "BeforeCombat")]
        public string BeforeCombatL
        {
            set => BeforeCombat = Convert.ToBoolean(value);
            get => BeforeCombat.ToString();
        }

        public Buff() { }
    }

    [XmlRoot(ElementName = "Buffs")]
    public class Buffs
    {
        [XmlElement(ElementName = "Buff")]
        public List<Buff> Buff = new List<Buff>();
    }

    [XmlRoot(ElementName = "HpBuff")]
    public class HpBuff
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Key")]

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        public string Key { get; set; }

        [Required]
        [Range(0, 30000, ErrorMessage = "Wrong castdelay!")]
        [XmlAttribute(AttributeName = "CastDelay")]
        public int CastDelay { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Trigger")]
        public int Trigger { get; set; }

        [XmlIgnore]
        public DateTime lastUse = DateTime.Now.AddHours(-1);

        [XmlIgnore]
        public bool IsTarget { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "IsTarget bool error!")]
        [XmlAttribute(AttributeName = "IsTarget")]
        public string TargetL
        {
            set => IsTarget = Convert.ToBoolean(value);
            get => IsTarget.ToString();
        }

        public HpBuff() { }
    }

    [XmlRoot(ElementName = "HpBuffs")]
    public class HpBuffs
    {
        [XmlElement(ElementName = "HpBuff")]
        public List<HpBuff> HpBuff = new List<HpBuff>();
    }

    [XmlRoot(ElementName = "MpBuff")]
    public class MpBuff
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }

        [Required]
        [Range(0, 30000, ErrorMessage = "Wrong castdelay!")]
        [XmlAttribute(AttributeName = "CastDelay")]
        public int CastDelay { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Trigger")]
        public int Trigger { get; set; }

        [XmlIgnore]
        public DateTime lastUse = DateTime.Now.AddHours(-1);

        [XmlIgnore]
        public bool IsTarget { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "IsTarget bool error!")]
        [XmlAttribute(AttributeName = "IsTarget")]
        public string TargetL
        {
            set => IsTarget = Convert.ToBoolean(value);
            get => IsTarget.ToString();
        }

        public MpBuff() { }
    }

    [XmlRoot(ElementName = "MpBuffs")]
    public class MpBuffs
    {
        [XmlElement(ElementName = "MpBuff")]
        public List<MpBuff> MpBuff = new List<MpBuff>();
    }

    [XmlRoot(ElementName = "HookSpell")]
    public class HookSpell
    {
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "MainSpell")]
    public class MainSpell
    {
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "AtackRange")]
    public class AtackRange
    {
        [Required]
        [Range(0, 100, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Trigger")]
        public int Trigger { get; set; }
    }

    [XmlRoot(ElementName = "TargetHeight")]
    public class TargetHeight
    {
        [Required]
        [Range(0, 2000, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Height")]
        public int Height { get; set; }
    }

    [XmlRoot(ElementName = "TargetRectangle")]
    public class TargetRectangle
    {
        [Required]
        [Range(0, 2000, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "X")]
        public int X { get; set; }

        [Required]
        [Range(0, 2000, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Y")]
        public int Y { get; set; }

        [Required]
        [Range(0, 2000, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "W")]
        public int W { get; set; }

        [Required]
        [Range(0, 2000, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "H")]
        public int H { get; set; }
    }

    [XmlRoot(ElementName = "TargetMouseSpeed")]
    public class TargetMouseSpeed
    {
        [Required]
        [Range(0, 20, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Value")]
        public int Value { get; set; }
    }

    [XmlRoot(ElementName = "Combats")]
    public class Combats
    {
        [Required, ValidateObject]
        [XmlElement(ElementName = "HookSpell")]
        public HookSpell HookSpell { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "MainSpell")]
        public MainSpell MainSpell { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "AtackRange")]
        public AtackRange AtackRange { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TargetHeight")]
        public TargetHeight TargetHeight { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TargetRectangle")]
        public TargetRectangle TargetRectangle { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TargetMouseSpeed")]
        public TargetMouseSpeed TargetMouseSpeed { get; set; }
    }

    [XmlRoot(ElementName = "YellowMobsTarget")]
    public class YellowMobsTarget
    {
        [XmlIgnore]
        public bool Enabled { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "Enabled bool error!")]
        [XmlAttribute(AttributeName = "Enabled")]
        public string EnabledL
        {
            set => Enabled = Convert.ToBoolean(value);
            get => Enabled.ToString();
        }
    }

    [XmlRoot(ElementName = "Loot")]
    public class Loot
    {
        [XmlIgnore]
        public bool Enabled { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "Enabled bool error!")]
        [XmlAttribute(AttributeName = "Enabled")]
        public string EnabledL
        {
            set => Enabled = Convert.ToBoolean(value);
            get => Enabled.ToString();
        }
    }

    [XmlRoot(ElementName = "Skin")]
    public class Skin
    {
        [XmlIgnore]
        public bool Enabled { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "Enabled bool error!")]
        [XmlAttribute(AttributeName = "Enabled")]
        public string TargetL
        {
            set => Enabled = Convert.ToBoolean(value);
            get => Enabled.ToString();
        }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong tries!")]
        [XmlAttribute(AttributeName = "Tries")]
        public int Tries { get; set; }
    }

    [XmlRoot(ElementName = "Facing")]
    public class Facing
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "TargetLastTarget")]
    public class TargetLastTarget
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "Mount")]
    public class Mount
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }

        [XmlIgnore]
        public bool SpotEnabled { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "SpotEnabled bool error!")]
        [XmlAttribute(AttributeName = "SpotEnabled")]
        public string TargetL
        {
            set => SpotEnabled = Convert.ToBoolean(value);
            get => SpotEnabled.ToString();
        }
    }

    [XmlRoot(ElementName = "ClearTarget")]
    public class ClearTarget
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "ConjureWater")]
    public class ConjureWater
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "ConjureFood")]
    public class ConjureFood
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "UseWater")]
    public class UseWater
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Trigger")]
        public int Trigger { get; set; }
    }

    [XmlRoot(ElementName = "UseFood")]
    public class UseFood
    {
        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Key error!")]
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Wrong trigger!")]
        [XmlAttribute(AttributeName = "Trigger")]
        public int Trigger { get; set; }
    }

    [XmlRoot(ElementName = "GlobalSettings")]
    public class GlobalSettings
    {
        [Required, ValidateObject]
        [XmlElement(ElementName = "YellowMobsTarget")]
        public YellowMobsTarget YellowMobsTarget { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Loot")]
        public Loot Loot { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Skin")]
        public Skin Skin { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Facing")]
        public Facing Facing { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "TargetLastTarget")]
        public TargetLastTarget TargetLastTarget { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Mount")]
        public Mount Mount { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "ClearTarget")]
        public ClearTarget ClearTarget { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "ConjureWater")]
        public ConjureWater ConjureWater { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "ConjureFood")]
        public ConjureFood ConjureFood { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "UseWater")]
        public UseWater UseWater { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "UseFood")]
        public UseFood UseFood { get; set; }
    }

    [XmlRoot(ElementName = "WhiteMob")]
    public class WhiteMob
    {
        [Required]
        [Range(0, 1000000, ErrorMessage = "Wrong mobid!")]
        [XmlAttribute(AttributeName = "MobId")]
        public int MobId { get; set; }
        public WhiteMob() { }
        public WhiteMob(int mobid)
        {
            MobId = mobid;
        }
    }

    [XmlRoot(ElementName = "WhiteMobList")]
    public class WhiteMobList
    {
        [ValidateObject]
        [XmlElement(ElementName = "WhiteMob")]
        public List<WhiteMob> WhiteMob = new List<WhiteMob>();
    }

    [XmlRoot(ElementName = "Vendor")]
    public class Vendor
    {
        [Required]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Vendor Name error!")]
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Vendor Dot error!")]
        [XmlAttribute(AttributeName = "VendorDot")]
        public string VendorDot { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Vendor return dot error!")]
        [XmlAttribute(AttributeName = "ReturnDot")]
        public string ReturnDot { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "MountUnmountDot error!")]
        [XmlAttribute(AttributeName = "MountUnmountDot")]
        public string MountUnmountDot { get; set; }
    }

    [XmlRoot(ElementName = "Profile")]
    public class Profile
    {
        [Required, ValidateObject]
        [XmlElement(ElementName = "MainDots")]
        public MainDots MainDots { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Buffs")]
        public Buffs Buffs { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "HpBuffs")]
        public HpBuffs HpBuffs { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "MpBuffs")]
        public MpBuffs MpBuffs { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Combats")]
        public Combats Combats { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "GlobalSettings")]
        public GlobalSettings GlobalSettings { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "WhiteMobList")]
        public WhiteMobList WhiteMobList { get; set; }

        [Required, ValidateObject]
        [XmlElement(ElementName = "Vendor")]
        public Vendor Vendor { get; set; }
    }


    class ProfileSettings
    {
        public static Profile Prof = new Profile();
        public static string ProfileLocation { get; set; }

        public static bool ConfigReader()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                using (FileStream reader = new FileStream(ProfileLocation, FileMode.OpenOrCreate))
                {
                    Prof = (Profile)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Debug.AddDebugRecord(ProfileLocation + e.Message, true);
                return false;
            }

            List<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(Prof, new ValidationContext(Prof), results, true))
            {
                ValidateObjectAttribute.PrintResults(ProfileLocation + " : ", results);
                return false;
            }
            WowProcess.GetWowRect();
            if (WowProcess.wowRect.Width < Prof.Combats.TargetRectangle.X + Prof.Combats.TargetRectangle.W && WowProcess.wowRect.Width < Prof.Combats.TargetRectangle.Y + Prof.Combats.TargetRectangle.H)
            {
                // MessageBox.Show("Invalid size seek rectangle!");
                return false;
            }

            return true;
        }
    }

}
