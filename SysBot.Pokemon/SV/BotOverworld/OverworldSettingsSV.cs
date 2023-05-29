﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using SysBot.Base;
using static SysBot.Pokemon.RaidSettingsSV;

namespace SysBot.Pokemon
{
    public class OverworldSettingsSV : IBotStateSettings, ICountSettings
    {
        private const string Overworld = nameof(Overworld);
        private const string Counts = nameof(Counts);
        public override string ToString() => "OverworldBotSV Settings";

        [Category(Overworld), Description("When enabled, the bot will continue after finding a suitable match.")]
        public ContinueAfterMatch ContinueAfterMatch { get; set; } = ContinueAfterMatch.PauseWaitAcknowledge;

        [Category(Overworld), Description("If not blank will check for a match from Stop Conditions plus the Species listed here. Do not include spaces for Species name and separate species with a comma. Ex: IronThorns,Cetoddle,Pikachu,RoaringMoon")]
        public string SpeciesToHunt { get; set; } = string.Empty;

        [Category(Overworld), Description("When enabled, the bot will stop on any Special Marks Only, ignoring Uncommon, all Time, and all Weather marks.")]
        public bool SpecialMarksOnly { get; set; } = false;

        [Category(Overworld), Description("Select which location you are scanning.")]
        public Location LocationSelection { get; set; } = Location.NonAreaZero;

        [Category(Overworld), Description("When enabled, the bot will stop when encounter has a Scale of XXXS or XXXL and matches StopConditions.")]
        public bool StopOnMinMaxScale { get; set; } = false;

        [Category(Overworld), Description("When enabled, the bot will stop for 3 Segment Dunsparce or Family of Three Maus and matches StopConditions.")]
        public bool StopOnOneInOneHundredOnly { get; set; } = false;

        [Category(Overworld), Description("When enabled, the bot will make a sandwich. If false the bot will stop after 30 minutes.")]
        public bool MakeASandwich { get; set; } = true;

        [Category(Overworld), Description("Some locations take a little longer to load in spawns, set this to wait longer than default time. 1000ms = 1s. [Default: 0ms]")]
        public int WaitTimeBeforeSaving { get; set; } = 0;

        [Category(Overworld), Description("Picnic Filters"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PicnicFiltersCategory PicnicFilters { get; set; } = new();

        [Category(Overworld), Description("Rollover Filters"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RolloverFiltersCategory RolloverFilters { get; set; } = new();

        [Category(Overworld), Description("Movement Filters"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MovementFiltersCategory MovementFilters { get; set; } = new();

        [Category(Overworld), Description("When enabled, the screen will be turned off during normal bot loop operation to save power.")]
        public bool ScreenOff { get; set; }

        [Category(Overworld)]
        [TypeConverter(typeof(PicnicFiltersCategoryConverter))]
        public class PicnicFiltersCategory
        {
            public override string ToString() => "Picnic Conditions";

            [Category(Overworld), Description("When enabled, the bot will click DUP on Item 1.")]
            public bool Item1DUP { get; set; } = false;

            [Category(Overworld), Description("Amount of clicks to get to Item 1.")]
            public int Item1Clicks { get; set; } = 0;

            [Category(Overworld), Description("When enabled, the bot will click DUP on Item 2.")]
            public bool Item2DUP { get; set; } = true;

            [Category(Overworld), Description("Amount of clicks to get to Item 2.")]
            public int Item2Clicks { get; set; } = 0;

            [Category(Overworld), Description("When enabled, the bot will click DUP on Item 3.")]
            public bool Item3DUP { get; set; } = true;

            [Category(Overworld), Description("Amount of clicks to get to Item 3.")]
            public int Item3Clicks { get; set; } = 0;

            [Category(Overworld), Description("Amount of ingredients to hold.")]
            public int AmountOfIngredientsToHold { get; set; } = 3;

            [Category(Overworld), Description("Amount of time to hold L stick up to ingredients for sandwich. [Default: 630ms]")]
            public int HoldUpToIngredients { get; set; } = 630;

        }

        [Category(Overworld)]
        [TypeConverter(typeof(MovementFiltersCategoryConverter))]
        public class MovementFiltersCategory
        {
            public override string ToString() => "Movement Conditions";

            [Category(Overworld), Description("Indicates how long the character will move north before every scan.")]
            public int MoveUpMs { get; set; } = 3000;

            [Category(Overworld), Description("Indicates how long the character will move south before every scan.")]
            public int MoveDownMs { get; set; } = 3000;

        }

        [Category(Overworld)]
        [TypeConverter(typeof(RolloverFiltersCategoryConverter))]
        public class RolloverFiltersCategory
        {
            public override string ToString() => "Rollover Conditions";

            [Category(Overworld), Description("When enabled, the bot will check if our dayseed changes to attempt preventing a lost outbreak.")]
            public bool CheckForRollover { get; set; } = false;

            [Category(Overworld), Description("When enabled, the bot will use the overshoot method to apply rollover correction, otherwise will use DDOWN clicks.")]
            public bool UseOvershoot { get; set; } = false;

            [Category(Overworld), Description("Set your Switch Date/Time format in the Date/Time settings. The day will automatically rollback by 1 if the Date changes.")]
            public DTFormat DateTimeFormat { get; set; } = DTFormat.MMDDYY;

            [Category(Overworld), Description("Time to scroll down duration in milliseconds for accessing date/time settings during rollover correction. You want to have it overshoot the Date/Time setting by 1, as it will click DUP after scrolling down. [Default: 930ms]")]
            public int HoldTimeForRollover { get; set; } = 900;

            [Category(Overworld), Description("Amount of times to hit DDOWN for accessing date/time settings during rollover correction. [Default: 39 Clicks]")]
            public int DDOWNClicks { get; set; } = 39;

            [Category(Overworld), Description("If true, start the bot when you are on the HOME screen with the game closed. The bot will only run the rollover routine so you can try to configure accurate timing.")]
            public bool ConfigureRolloverCorrection { get; set; } = false;

        }

        private sealed class PicnicFiltersCategoryConverter : TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object? value, Attribute[]? attributes) => TypeDescriptor.GetProperties(typeof(PicnicFiltersCategory));

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => destinationType != typeof(string) && base.CanConvertTo(context, destinationType);
        }

        private sealed class MovementFiltersCategoryConverter : TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object? value, Attribute[]? attributes) => TypeDescriptor.GetProperties(typeof(MovementFiltersCategory));

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => destinationType != typeof(string) && base.CanConvertTo(context, destinationType);
        }

        private sealed class RolloverFiltersCategoryConverter : TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object? value, Attribute[]? attributes) => TypeDescriptor.GetProperties(typeof(RolloverFiltersCategory));

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => destinationType != typeof(string) && base.CanConvertTo(context, destinationType);
        }

        private int _completedScans;
        private int _completedShinyScans;

        [Category(Counts), Description("Encounters Scanned")]
        public int CompletedScans
        {
            get => _completedScans;
            set => _completedScans = value;
        }

        [Category(Counts), Description("Shiny Encounters Scanned")]
        public int CompletedShinyScans
        {
            get => _completedShinyScans;
            set => _completedShinyScans = value;
        }

        [Category(Counts), Description("When enabled, the counts will be emitted when a status check is requested.")]
        public bool EmitCountsOnStatusCheck { get; set; }

        public int AddCompletedScans() => Interlocked.Increment(ref _completedScans);

        public int AddShinyScans() => Interlocked.Increment(ref _completedShinyScans);

        public IEnumerable<string> GetNonZeroCounts()
        {
            if (!EmitCountsOnStatusCheck)
                yield break;
            if (CompletedScans != 0)
                yield return $"Encounters Scanned: {CompletedScans}";
            if (CompletedShinyScans != 0)
                yield return $"Shiny Encounters Scanned: {CompletedShinyScans}";
        }

        public enum Location
        {
            NonAreaZero = 0,
            ResearchStation1 = 1,
            ResearchStation2 = 2,
            ResearchStation3 = 3,
            ResearchStation4 = 4,
            SecretCave = 5,
            TownBorder = 6,
        }

    }
}
