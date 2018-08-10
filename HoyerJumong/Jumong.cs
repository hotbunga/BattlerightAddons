﻿using System;
using System.Linq;
using BattleRight.Core;
using BattleRight.Core.Enumeration;
using BattleRight.Core.GameObjects;
using BattleRight.Sandbox;
using BattleRight.SDK;
using BattleRight.SDK.Enumeration;
using BattleRight.SDK.UI;
using BattleRight.SDK.UI.Models;
using BattleRight.SDK.UI.Values;
using Hoyer.Champions.Jumong.Modes;
using Hoyer.Common.Extensions;
using Hoyer.Common.Local;

// ReSharper disable ArrangeAccessorOwnerBody

namespace Hoyer.Champions.Jumong
{
    public class Jumong : IAddon
    {
        public static bool Enabled;
        public static bool AimUserInput;
        private static IMode _mode;

        public void OnInit()
        {
            MenuEvents.Initialize += MenuHandler.Init;
            MenuEvents.Update += MenuHandler.Update;
            Skills.Initialize += SpellInit;
            CommonEvents.Update += OnUpdate;
        }

        private void SpellInit()
        {
            if (LocalPlayer.Instance.CharName != "Jumong") return;
            Skills.Active.Add(new SkillBase(AbilitySlot.Ability1, SkillType.Line, 7.9f, 17, 0.3f));
            Skills.Active.Add(new SkillBase(AbilitySlot.Ability2, SkillType.Line, 10.25f, 26.5f, 0.3f));
            Skills.Active.Add(new SkillBase(AbilitySlot.Ability3, SkillType.Line, 7.8f, 13.5f, 0.3f));
            Skills.Active.Add(new SkillBase(AbilitySlot.Ability4, SkillType.Circle, 10, 0, 2, 0.5f));
            Skills.Active.Add(new SkillBase(AbilitySlot.Ability5, SkillType.Circle, 6.8f, 12, 1));
            Skills.Active.Add(new SkillBase(AbilitySlot.Ability7, SkillType.Line, 7.8f, 13.5f, 0.3f));
            Skills.Active.Add(new SkillBase(AbilitySlot.EXAbility1, SkillType.Line, 10.5f, 24.5f, 0.3f));
            Skills.Active.Add(new SkillBase(AbilitySlot.EXAbility2, SkillType.Line, 8.8f, 26.5f, 0.3f));
        }

        private void OnUpdate()
        {
            if (!Enabled || !Game.IsInGame || Game.CurrentMatchState != MatchState.InRound || LocalPlayer.Instance.CharName != "Jumong" || LocalPlayer.Instance.HasBuff("SpellBlock"))
            {
                LocalPlayer.EditAimPosition = false;
                return;
            }
            _mode.Update();
        }

        public static void SetMode(bool combo)
        {
            if (!combo) _mode = new AimOnly();
            else _mode = new AimAndCast();
        }

        public void OnUnload()
        {
        }
    }
}