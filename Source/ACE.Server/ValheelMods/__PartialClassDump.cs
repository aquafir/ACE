using ACE.Server.Entity.Actions;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;
using ACE.Server.WorldObjects.Entity;
using ACE.Entity.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity;
using ACE.Server.Network.GameEvent.Events;
using System.Numerics;
using ACE.Common;
using ACE.Server.Physics.Animation;
using ACE.Database;
using ACE.Entity;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Physics;

namespace ACE.Server.WorldObjects
{
    partial class Player
    {
        public static readonly float GunBladeDistance = 80.0f;

        public void UnSneak()
        {
            if (CloakStatus == CloakStatus.Off)
                return;

            var actionChain = new ActionChain();

            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageDeleteObject(this));
            });
            actionChain.AddAction(this, () =>
            {
                NoDraw = true;
                EnqueueBroadcastPhysicsState();
                Visibility = false;
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageCreateObject(this));
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                Cloaked = false;
                Ethereal = false;
                NoDraw = false;
                ReportCollisions = true;
                EnqueueBroadcastPhysicsState();
            });

            actionChain.EnqueueChain();
        }

        public void HandleSneak()
        {
            if (CloakStatus == CloakStatus.On)
                return;

            var actionChain = new ActionChain();

            actionChain.AddAction(this, () =>
            {
                Cloaked = true;
                Ethereal = false;
                NoDraw = true;
                ReportCollisions = true;
                EnqueueBroadcastPhysicsState();
            });
            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageDeleteObject(this));
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                Visibility = true;
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageCreateObject(this, true, true));
            });

            actionChain.EnqueueChain();
        }

        /// <summary>
        /// Raise the available XP by a percentage of the current level XP or a maximum
        /// </summary>
        public void GrantLevelProportionalXp(double percent, long min, long max, bool shareable = false)
        {
            var nextLevelXP = GetXPBetweenLevels(Level.Value, Level.Value + 1);

            var scaledXP = (long)Math.Round(nextLevelXP * percent);

            if (max > 0)
                scaledXP = Math.Min(scaledXP, max);

            if (min > 0)
                scaledXP = Math.Max(scaledXP, min);

            var shareType = shareable ? ShareType.All : ShareType.None;


            // apply xp modifiers?
            EarnXP(scaledXP, XpType.Quest, shareType);
        }

        static int GetHoTSpell(string spellName)
        {
            string[] spellNames =
            {
            // Health
            "Incantation of Heal Self",
            "Adja's Intervention",
            "Heal Self VI",
            "Heal Self V",
            "Heal Self IV",
            "Heal Self III",
            "Heal Self II",
            "Heal Self I",
            "Incantation of Heal Other",
            "Adja's Gift",
            "Heal Other VI",
            "Heal Other V",
            "Heal Other IV",
            "Heal Other III",
            "Heal Other II",
            "Heal Otehr I",
            
            // Stamina
            "Incantation of Revitalize Self",
            "Robustification",
            "Revitalize Self VI",
            "Revitalize Self V",
            "Revitalize Self IV",
            "Revitalize Self III",
            "Revitalize Self II",
            "Revitalize Self I",
            "Incantation of Revitalize Other",
            "Replenish",
            "Revitalize Other VI",
            "Revitalize Other V",
            "Revitalize Other IV",
            "Revitalize Other III",
            "Revitalize Other II",
            "Revitalize Other I",
        };

            int[] spellMappings =
            {
            // Health
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            
            // Stamina
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
        };

            int index = Array.IndexOf(spellNames, spellName);
            int procSpell;

            if (index != -1)
            {
                procSpell = spellMappings[index];
            }
            else
            {
                return 1;
            }

            return procSpell;
        }

        public void LifeMagicHot(Player caster, Player target, int spell)
        {
            // This sets the HoT flag on the target
            var fellows = caster.GetFellowshipTargets();
            double currentUnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            if (fellows != null)
            {
                foreach (var fellow in fellows)
                {
                    if (caster.GetDistance(fellow) < 30.0f)
                    {
                        if (spell == 8)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot8, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 7)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot7, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 6)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot6, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 5)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot5, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 4)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot4, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 3)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot3, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 2)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot2, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 1)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot1, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                    }
                }
            }
        }

        public void LifeMagicSot(Player caster, Player target, int spell)
        {
            // This sets the HoT flag on the target
            var fellows = caster.GetFellowshipTargets();
            double currentUnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            if (fellows != null)
            {
                foreach (var fellow in fellows)
                {
                    if (caster.GetDistance(fellow) < 30.0f)
                    {
                        if (spell == 8)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot8, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 7)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot7, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 6)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot6, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 5)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot5, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 4)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot4, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 3)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot3, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 2)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot2, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 1)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot1, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                    }
                }
            }
        }

        public static void WarMagicChannel(Player caster, Creature target, Spell spell, int numCasts, bool isWeaponSpell)
        {
            var weapon = caster.GetEquippedWand();
            var fellows = caster.GetFellowshipTargets();
            caster.IsWarChanneling = true;

            for (int i = 0; i < numCasts; i++)
            {
                if (fellows != null && target != null && spell != null)
                {
                    foreach (var fellow in fellows)
                    {
                        if (caster.GetDistance(fellow) < 30.0f)
                        {
                            if (fellow.GetDistance(target) < 30.0f)
                            {
                                fellow.WarMagic(target, spell, weapon);
                            }
                        }
                    }
                }
            }

            caster.IsWarChanneling = false;
        }

        public void LaunchCleaveMissile(WorldObject target, int attackSequence, MotionStance stance, bool subsequent = false)
        {
            // cleaving skips original target
            if (AttackSequence != attackSequence)
                return;

            var weapon = GetEquippedMissileWeapon();
            if (weapon == null || CombatMode == CombatMode.NonCombat)
            {
                OnAttackDone();
                return;
            }

            var ammo = weapon.IsAmmoLauncher ? GetEquippedAmmo() : weapon;
            if (ammo == null)
            {
                OnAttackDone();
                return;
            }

            var launcher = GetEquippedMissileLauncher();

            var creature = target as Creature;
            if (!IsAlive || IsBusy || MissileTarget == null || creature == null || !creature.IsAlive || suicideInProgress)
            {
                OnAttackDone();
                return;
            }

            if (!TargetInRange(target))
            {
                // this must also be sent to actually display the transient message
                SendWeenieError(WeenieError.MissileOutOfRange);

                // this prevents the accuracy bar from refilling when 'repeat attacks' is enabled
                OnAttackDone();

                return;
            }

            var actionChain = new ActionChain();

            if (subsequent && !IsFacing(target))
            {
                var rotateTime = Rotate(target);
                actionChain.AddDelaySeconds(rotateTime);
            }

            // launch animation
            // point of no return beyond this point -- cannot be cancelled
            actionChain.AddAction(this, () => Attacking = true);

            if (subsequent)
            {
                // client shows hourglass, until attack done is received
                // retail only did this for subsequent attacks w/ repeat attacks on
                Session.Network.EnqueueSend(new GameEventCombatCommenceAttack(Session));
            }

            var projectileSpeed = GetProjectileSpeed();

            // get z-angle for aim motion
            var aimVelocity = GetAimVelocity(target, projectileSpeed);

            var aimLevel = GetAimLevel(aimVelocity);

            // calculate projectile spawn pos and velocity
            var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);

            var velocity = CalculateProjectileVelocity(localOrigin, target, projectileSpeed, out Vector3 origin, out Quaternion orientation);

            //Console.WriteLine($"Velocity: {velocity}");

            if (velocity == Vector3.Zero)
            {
                // pre-check succeeded, but actual velocity calculation failed
                SendWeenieError(WeenieError.MissileOutOfRange);

                // this prevents the accuracy bar from refilling when 'repeat attacks' is enabled
                Attacking = false;
                OnAttackDone();
                return;
            }

            var launchTime = EnqueueMotionPersist(actionChain, aimLevel);

            // launch projectile
            actionChain.AddAction(this, () =>
            {
                // handle self-procs
                TryProcEquippedItems(this, this, true, weapon);

                var sound = GetLaunchMissileSound(weapon);
                EnqueueBroadcast(new GameMessageSound(Guid, sound, 1.0f));

                // stamina usage
                // TODO: ensure enough stamina for attack
                // TODO: verify formulas - double/triple cost for bow/xbow?
                var staminaCost = GetAttackStamina(GetAccuracyRange());
                UpdateVitalDelta(Stamina, -staminaCost);

                var projectile = LaunchProjectile(launcher, ammo, target, origin, orientation, velocity);
                UpdateAmmoAfterLaunch(ammo);
            });

            actionChain.EnqueueChain();

            if (UnderLifestoneProtection)
                LifestoneProtectionDispel();
        }

        public List<Creature> GetMissileCleaveTarget(Creature target, WorldObject weapon)
        {
            var player = this as Player;

            if (!weapon.IsCleaving) return null;

            // sort visible objects by ascending distance
            var visible = PhysicsObj.ObjMaint.GetVisibleObjectsValuesWhere(o => o.WeenieObj.WorldObject != null);
            visible.Sort(DistanceComparator);

            var cleaveTargets = new List<Creature>();
            var totalCleaves = weapon.CleaveTargets;

            foreach (var obj in visible)
            {
                // cleaving skips original target
                if (obj.ID == target.PhysicsObj.ID || target == null)
                    continue;

                // only cleave creatures
                var creature = obj.WeenieObj.WorldObject as Creature;
                if (creature == null || creature.Teleporting || creature.IsDead) continue;

                if (player != null && player.CheckPKStatusVsTarget(creature, null) != null)
                    continue;

                if (!creature.Attackable && creature.TargetingTactic == TargetingTactic.None || creature.Teleporting)
                    continue;

                if (creature is CombatPet && (player != null || this is CombatPet))
                    continue;

                // no objects in cleave range
                var cylDist = GetCylinderDistance(creature);
                if (cylDist > MissileCleaveCylRange)
                    return cleaveTargets;

                // only cleave in front of attacker
                var angle = GetAngle(creature);
                if (Math.Abs(angle) > MissileCleaveAngle / 2.0f)
                    continue;

                // found cleavable object
                cleaveTargets.Add(creature);
                if (cleaveTargets.Count == totalCleaves)
                    break;
            }
            return cleaveTargets;
        }

        public List<Creature> GetMissileAoETarget(Creature target, WorldObject weapon)
        {
            var player = this as Player;

            // sort visible objects by ascending distance
            var visible = PhysicsObj.ObjMaint.GetVisibleObjectsValuesWhere(o => o.WeenieObj.WorldObject != null);
            visible.Sort(DistanceComparator);

            var cleaveTargets = new List<Creature>();
            var totalCleaves = weapon.CleaveTargets;

            foreach (var obj in visible)
            {
                // only cleave creatures
                var creature = obj.WeenieObj.WorldObject as Creature;
                if (creature == null || creature.Teleporting || creature.IsDead) continue;

                if (player != null && player.CheckPKStatusVsTarget(creature, null) != null)
                    continue;

                if (!creature.Attackable && creature.TargetingTactic == TargetingTactic.None || creature.Teleporting)
                    continue;

                if (creature is CombatPet && (player != null || this is CombatPet))
                    continue;

                // no objects in cleave range
                var cylDist = GetCylinderDistance(creature);
                if (cylDist > MissileAoECylRange)
                    return cleaveTargets;

                // only cleave in front of attacker
                var angle = GetAngle(creature);
                if (Math.Abs(angle) > MissileAoEAngle)
                    continue;

                // found cleavable object
                cleaveTargets.Add(creature);
                if (cleaveTargets.Count == totalCleaves)
                    break;
            }
            return cleaveTargets;
        }

        public void CreateDoTSpot(Player player, List<Creature> targets)
        {
            if (targets != null)
            {
                var dot = DatabaseManager.World.GetCachedWeenie(300501);

                List<WorldObject> dotObjects = new List<WorldObject>();

                foreach (var m in targets)
                {
                    var newDot = WorldObjectFactory.CreateNewWorldObject(dot);

                    dotObjects.Add(newDot);
                }

                for (int i = 0; i < dotObjects.Count; i++)
                {
                    dotObjects[i].DoTOwnerGuid = (int)player.Guid.Full;
                    dotObjects[i].Damage = (int)(targets[i].Health.Current * 0.005f);
                    dotObjects[i].Location = targets[i].Location;
                    dotObjects[i].Location.LandblockId = new LandblockId(dotObjects[i].Location.GetCell());
                    dotObjects[i].EnterWorld();
                }
            }
        }

        public void GunBladeAttack(WorldObject target, int attackSequence, bool subsequent = false)
        {
            //log.Info($"{Name}.Attack({target.Name}, {attackSequence})");
            var weapon = GetEquippedMeleeWeapon();

            if (AttackSequence != attackSequence)
                return;

            if (CombatMode != CombatMode.Melee || MeleeTarget == null && !weapon.IsGunblade || IsBusy || !IsAlive || suicideInProgress)
            {
                OnAttackDone();
                return;
            }

            var creature = target as Creature;
            if (creature == null || !creature.IsAlive)
            {
                OnAttackDone();
                return;
            }

            var animLength = DoSwingMotion(target, out var attackFrames);
            if (animLength == 0)
            {
                OnAttackDone();
                return;
            }

            // point of no return beyond this point -- cannot be cancelled
            Attacking = true;

            if (subsequent)
            {
                // client shows hourglass, until attack done is received
                // retail only did this for subsequent attacks w/ repeat attacks on
                Session.Network.EnqueueSend(new GameEventCombatCommenceAttack(Session));
            }

            var attackType = GetWeaponAttackType(weapon);
            var numStrikes = GetNumStrikes(attackType);
            var swingTime = animLength / numStrikes / 1.5f;

            var actionChain = new ActionChain();

            // stamina usage
            // TODO: ensure enough stamina for attack
            var staminaCost = GetAttackStamina(GetPowerRange());
            UpdateVitalDelta(Stamina, -staminaCost);

            if (numStrikes != attackFrames.Count)
            {
                //log.Warn($"{Name}.GetAttackFrames(): MotionTableId: {MotionTableId:X8}, MotionStance: {CurrentMotionState.Stance}, Motion: {GetSwingAnimation()}, AttackFrames.Count({attackFrames.Count}) != NumStrikes({numStrikes})");
                numStrikes = attackFrames.Count;
            }

            // handle self-procs
            TryProcEquippedItems(this, this, true, weapon);

            var prevTime = 0.0f;
            var ammo = GetEquippedAmmo();

            if (ammo == null && weapon.IsGunblade)
            {
                Attacking = false;
                OnAttackDone();
                return;
            }

            var projectileSpeed = GetGunBladeProjectileSpeed();
            var aimVelocity = GetAimVelocity(target, projectileSpeed);
            var aimLevel = GetAimLevel(aimVelocity);
            var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);
            var velocity = CalculateProjectileVelocity(localOrigin, target, projectileSpeed, out Vector3 origin, out Quaternion orientation);

            for (var i = 0; i < numStrikes; i++)
            {
                // are there animation hooks for damage frames?
                //if (numStrikes > 1 && !TwoHandedCombat)
                //actionChain.AddDelaySeconds(swingTime);
                actionChain.AddDelaySeconds(attackFrames[i].time * animLength - prevTime);
                prevTime = attackFrames[i].time * animLength;

                actionChain.AddAction(this, () =>
                {
                    if (IsDead)
                    {
                        Attacking = false;
                        OnAttackDone();
                        return;
                    }

                    // handle target procs                  

                    var ammo = GetEquippedAmmo();

                    if (weapon != null && weapon.IsCleaving && weapon.IsGunblade == true)
                    {
                        var cleave = GetCleaveTarget(creature, weapon);

                        foreach (var cleaveHit in cleave)
                        {
                            // target procs don't happen for cleaving
                            DamageTarget(cleaveHit, weapon);
                            if (ammo != null)
                            {
                                LaunchProjectile(weapon, ammo, cleaveHit, origin, orientation, velocity);
                                UpdateAmmoAfterLaunch(ammo);
                            }
                        }
                    }

                    if (weapon != null && weapon.IsGunblade == true && ammo == null)
                    {
                        TryProcEquippedItems(this, creature, false, weapon);
                    }

                    if (weapon != null && weapon.IsGunblade == true && ammo != null)
                    {
                        LaunchProjectile(weapon, ammo, target, origin, orientation, velocity);
                        UpdateAmmoAfterLaunch(ammo);
                    }
                });
            }

            actionChain.AddDelaySeconds(animLength - prevTime);

            actionChain.AddAction(this, () =>
            {
                Attacking = false;

                // powerbar refill timing
                var refillMod = IsDualWieldAttack ? 0.8f : 1.0f;    // dual wield powerbar refills 20% faster

                PowerLevel = AttackQueue.Fetch();

                var nextRefillTime = PowerLevel * refillMod;
                NextRefillTime = DateTime.UtcNow.AddSeconds(nextRefillTime);
                var dotRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                var dist = GetCylinderDistance(target);

                if (creature.IsAlive && GetCharacterOption(CharacterOption.AutoRepeatAttacks) && IsMeleeVisible(target) && !IsBusy && !AttackCancelled && weapon.IsGunblade == true)
                {
                    // client starts refilling power meter
                    Session.Network.EnqueueSend(new GameEventAttackDone(Session));

                    var nextAttack = new ActionChain();
                    nextAttack.AddDelaySeconds(nextRefillTime);
                    nextAttack.AddAction(this, () => GunBladeAttack(target, attackSequence, true));
                    nextAttack.EnqueueChain();

                    if (IsDps)
                    {
                        if (MeleeDoTChance >= dotRoll)
                        {
                            //var dot = DatabaseManager.World.GetCachedWeenie(300501);
                            var dotTarget = target as Creature;
                            var targets = GetDoTTarget(dotTarget);
                            //var obj = WorldObjectFactory.CreateNewWorldObject(dot);

                            CreateDoTSpot(this, targets);
                        }
                    }
                    if (IsTank)
                    {
                        var chanceRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                        if (chanceRoll < 0.33f)
                        {
                            IsTankBuffed = true;
                        }
                    }
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }
                }

                else if (creature.IsAlive && GetCharacterOption(CharacterOption.AutoRepeatAttacks) && IsMeleeVisible(target) && !IsBusy && !AttackCancelled)
                {
                    // client starts refilling power meter
                    Session.Network.EnqueueSend(new GameEventAttackDone(Session));

                    var nextAttack = new ActionChain();
                    nextAttack.AddDelaySeconds(nextRefillTime);
                    nextAttack.AddAction(this, () => GunBladeAttack(target, attackSequence, true));
                    nextAttack.EnqueueChain();

                    if (IsDps)
                    {
                        if (MeleeDoTChance >= dotRoll)
                        {
                            //var dot = DatabaseManager.World.GetCachedWeenie(300501);
                            var dotTarget = target as Creature;
                            var targets = GetDoTTarget(dotTarget);
                            //var obj = WorldObjectFactory.CreateNewWorldObject(dot);

                            CreateDoTSpot(this, targets);
                        }
                    }
                    if (IsTank)
                    {
                        var chanceRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                        if (chanceRoll < 0.33f)
                        {
                            IsTankBuffed = true;
                        }
                    }
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }
                }
                else
                {
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }

                    OnAttackDone();
                }
            });

            actionChain.EnqueueChain();

            if (UnderLifestoneProtection)
                LifestoneProtectionDispel();
        }




        //Re-routes of renamed methods for involved code
        public void ValAttack(WorldObject target, int attackSequence, bool subsequent = false)
        {
            //log.Info($"{Name}.Attack({target.Name}, {attackSequence})");
            var weapon = GetEquippedMeleeWeapon();

            if (AttackSequence != attackSequence)
                return;

            if (CombatMode != CombatMode.Melee || MeleeTarget == null && weapon == null || IsBusy || !IsAlive || suicideInProgress)
            {
                OnAttackDone();
                return;
            }
            else if (CombatMode != CombatMode.Melee || MeleeTarget == null && !weapon.IsGunblade || IsBusy || !IsAlive || suicideInProgress)
            {
                OnAttackDone();
                return;
            }

            var creature = target as Creature;
            if (creature == null || !creature.IsAlive)
            {
                OnAttackDone();
                return;
            }

            var animLength = DoSwingMotion(target, out var attackFrames);
            if (animLength == 0)
            {
                OnAttackDone();
                return;
            }

            // point of no return beyond this point -- cannot be cancelled
            Attacking = true;

            if (subsequent)
            {
                // client shows hourglass, until attack done is received
                // retail only did this for subsequent attacks w/ repeat attacks on
                Session.Network.EnqueueSend(new GameEventCombatCommenceAttack(Session));
            }

            var attackType = GetWeaponAttackType(weapon);
            var numStrikes = GetNumStrikes(attackType);
            var swingTime = animLength / numStrikes / 1.5f;

            var actionChain = new ActionChain();

            // stamina usage
            // TODO: ensure enough stamina for attack
            var staminaCost = GetAttackStamina(GetPowerRange());
            UpdateVitalDelta(Stamina, -staminaCost);

            if (numStrikes != attackFrames.Count)
            {
                //log.Warn($"{Name}.GetAttackFrames(): MotionTableId: {MotionTableId:X8}, MotionStance: {CurrentMotionState.Stance}, Motion: {GetSwingAnimation()}, AttackFrames.Count({attackFrames.Count}) != NumStrikes({numStrikes})");
                numStrikes = attackFrames.Count;
            }

            // handle self-procs
            TryProcEquippedItems(this, this, true, weapon);

            var prevTime = 0.0f;
            bool targetProc = false;

            for (var i = 0; i < numStrikes; i++)
            {
                // are there animation hooks for damage frames?
                //if (numStrikes > 1 && !TwoHandedCombat)
                //actionChain.AddDelaySeconds(swingTime);
                actionChain.AddDelaySeconds(attackFrames[i].time * animLength - prevTime);
                prevTime = attackFrames[i].time * animLength;

                actionChain.AddAction(this, () =>
                {
                    if (IsDead)
                    {
                        Attacking = false;
                        OnAttackDone();
                        return;
                    }

                    var damageEvent = DamageTarget(creature, weapon);

                    // handle target procs
                    if (damageEvent != null && damageEvent.HasDamage && !targetProc)
                    {
                        TryProcEquippedItems(this, creature, false, weapon);
                        targetProc = true;
                    }

                    if (weapon != null && weapon.IsCleaving && weapon.IsGunblade == false)
                    {
                        var cleave = GetCleaveTarget(creature, weapon);

                        foreach (var cleaveHit in cleave)
                        {
                            // target procs don't happen for cleaving
                            DamageTarget(cleaveHit, weapon);
                        }
                    }

                    var ammo = GetEquippedAmmo();

                    if (weapon != null && weapon.IsCleaving && weapon.IsGunblade == true)
                    {
                        var cleave = GetCleaveTarget(creature, weapon);

                        foreach (var cleaveHit in cleave)
                        {
                            // target procs don't happen for cleaving
                            //var ammo = GetEquippedAmmo();
                            if (ammo != null && ammo.WeenieClassId == 300444)
                            {
                                var projectileSpeed = GetGunBladeProjectileSpeed();
                                var aimVelocity = GetAimVelocity(target, projectileSpeed);
                                var aimLevel = GetAimLevel(aimVelocity);
                                var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);
                                var velocity = CalculateProjectileVelocity(localOrigin, target, projectileSpeed, out Vector3 origin, out Quaternion orientation);

                                DamageTarget(cleaveHit, weapon);
                                LaunchProjectile(weapon, ammo, target, origin, orientation, velocity);

                                if (ammo.StackSize != null)
                                    UpdateAmmoAfterLaunch(ammo);
                            }
                            else
                            {
                                DamageTarget(cleaveHit, weapon);

                            }

                        }
                    }

                    if (weapon != null && weapon.IsGunblade == true && ammo != null)
                    {
                        if (ammo != null && ammo.WeenieClassId == 300444)
                        {
                            // var ammo = GetEquippedAmmo();
                            var projectileSpeed = GetGunBladeProjectileSpeed();
                            var aimVelocity = GetAimVelocity(target, projectileSpeed);
                            var aimLevel = GetAimLevel(aimVelocity);
                            var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);
                            var velocity = CalculateProjectileVelocity(localOrigin, target, projectileSpeed, out Vector3 origin, out Quaternion orientation);

                            LaunchProjectile(weapon, ammo, target, origin, orientation, velocity);

                            if (ammo.StackSize != null)
                                UpdateAmmoAfterLaunch(ammo);
                        }
                    }
                });
            }

            //actionChain.AddDelaySeconds(animLength - swingTime * numStrikes);
            actionChain.AddDelaySeconds(animLength - prevTime);

            actionChain.AddAction(this, () =>
            {
                Attacking = false;

                // powerbar refill timing
                var refillMod = IsDualWieldAttack ? 0.8f : 1.0f;    // dual wield powerbar refills 20% faster

                PowerLevel = AttackQueue.Fetch();

                var nextRefillTime = PowerLevel * refillMod;
                NextRefillTime = DateTime.UtcNow.AddSeconds(nextRefillTime);
                var dotRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                var dist = GetCylinderDistance(target);

                if (creature.IsAlive && GetCharacterOption(CharacterOption.AutoRepeatAttacks) && IsMeleeVisible(target) && !IsBusy && !AttackCancelled && weapon == null)
                {
                    // client starts refilling power meter
                    Session.Network.EnqueueSend(new GameEventAttackDone(Session));

                    var nextAttack = new ActionChain();
                    nextAttack.AddDelaySeconds(nextRefillTime);
                    nextAttack.AddAction(this, () => Attack(target, attackSequence, true));
                    nextAttack.EnqueueChain();

                    if (IsDps)
                    {
                        if (MeleeDoTChance >= dotRoll)
                        {
                            //var dot = DatabaseManager.World.GetCachedWeenie(300501);
                            var dotTarget = target as Creature;
                            var targets = GetDoTTarget(dotTarget);
                            //var obj = WorldObjectFactory.CreateNewWorldObject(dot);

                            CreateDoTSpot(this, targets);
                        }
                    }
                    if (IsTank)
                    {
                        var chanceRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                        if (chanceRoll < 0.33f)
                        {
                            IsTankBuffed = true;
                        }
                    }
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }
                }
                else if (creature.IsAlive && GetCharacterOption(CharacterOption.AutoRepeatAttacks) && IsMeleeVisible(target) && !IsBusy && !AttackCancelled && weapon.IsGunblade == true)
                {
                    // client starts refilling power meter
                    Session.Network.EnqueueSend(new GameEventAttackDone(Session));

                    var nextAttack = new ActionChain();
                    nextAttack.AddDelaySeconds(nextRefillTime);
                    nextAttack.AddAction(this, () => Attack(target, attackSequence, true));
                    nextAttack.EnqueueChain();

                    if (IsDps)
                    {
                        if (MeleeDoTChance >= dotRoll)
                        {
                            //var dot = DatabaseManager.World.GetCachedWeenie(300501);
                            var dotTarget = target as Creature;
                            var targets = GetDoTTarget(dotTarget);
                            //var obj = WorldObjectFactory.CreateNewWorldObject(dot);

                            CreateDoTSpot(this, targets);
                        }
                    }
                    if (IsTank)
                    {
                        var chanceRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                        if (chanceRoll < 0.33f)
                        {
                            IsTankBuffed = true;
                        }
                    }
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }
                }

                else if (creature.IsAlive && GetCharacterOption(CharacterOption.AutoRepeatAttacks) && (dist <= MeleeDistance || dist <= StickyDistance && IsMeleeVisible(target)) && !IsBusy && !AttackCancelled)
                {
                    // client starts refilling power meter
                    Session.Network.EnqueueSend(new GameEventAttackDone(Session));

                    var nextAttack = new ActionChain();
                    nextAttack.AddDelaySeconds(nextRefillTime);
                    nextAttack.AddAction(this, () => Attack(target, attackSequence, true));
                    nextAttack.EnqueueChain();

                    if (IsDps)
                    {
                        if (MeleeDoTChance >= dotRoll)
                        {
                            //var dot = DatabaseManager.World.GetCachedWeenie(300501);
                            var dotTarget = target as Creature;
                            var targets = GetDoTTarget(dotTarget);
                            //var obj = WorldObjectFactory.CreateNewWorldObject(dot);

                            CreateDoTSpot(this, targets);
                        }
                    }
                    if (IsTank)
                    {
                        var chanceRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                        if (chanceRoll < 0.33f)
                        {
                            IsTankBuffed = true;
                        }
                    }
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }
                }
                else
                {
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                    if (IsSneaking == true)
                    {
                        IsSneaking = false;
                        UnSneak();
                        SetProperty(PropertyInt.CloakStatus, (int)CloakStatus.Off);
                        PlayParticleEffect(PlayScript.EnchantUpGreen, Guid);
                    }

                    OnAttackDone();
                }
            });

            actionChain.EnqueueChain();

            if (UnderLifestoneProtection)
                LifestoneProtectionDispel();
        }

        public void ValLaunchMissile(WorldObject target, int attackSequence, MotionStance stance, bool subsequent = false)
        {
            if (AttackSequence != attackSequence)
                return;

            var weapon = GetEquippedMissileWeapon();
            if (weapon == null || CombatMode == CombatMode.NonCombat)
            {
                OnAttackDone();
                return;
            }

            var ammo = weapon.IsAmmoLauncher ? GetEquippedAmmo() : weapon;
            if (ammo == null)
            {
                OnAttackDone();
                return;
            }

            var launcher = GetEquippedMissileLauncher();

            var creature = target as Creature;
            if (!IsAlive || IsBusy || MissileTarget == null || creature == null || !creature.IsAlive || suicideInProgress)
            {
                OnAttackDone();
                return;
            }

            if (!TargetInRange(target))
            {
                // this must also be sent to actually display the transient message
                SendWeenieError(WeenieError.MissileOutOfRange);

                // this prevents the accuracy bar from refilling when 'repeat attacks' is enabled
                OnAttackDone();

                return;
            }

            var actionChain = new ActionChain();

            if (subsequent && !IsFacing(target))
            {
                var rotateTime = Rotate(target);
                actionChain.AddDelaySeconds(rotateTime);
            }

            // launch animation
            // point of no return beyond this point -- cannot be cancelled
            actionChain.AddAction(this, () => Attacking = true);

            if (subsequent)
            {
                // client shows hourglass, until attack done is received
                // retail only did this for subsequent attacks w/ repeat attacks on
                Session.Network.EnqueueSend(new GameEventCombatCommenceAttack(Session));
            }

            var projectileSpeed = GetProjectileSpeed();

            // get z-angle for aim motion
            var aimVelocity = GetAimVelocity(target, projectileSpeed);

            var aimLevel = GetAimLevel(aimVelocity);

            // calculate projectile spawn pos and velocity
            var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);

            var velocity = CalculateProjectileVelocity(localOrigin, target, projectileSpeed, out Vector3 origin, out Quaternion orientation);

            //Console.WriteLine($"Velocity: {velocity}");

            if (velocity == Vector3.Zero)
            {
                // pre-check succeeded, but actual velocity calculation failed
                SendWeenieError(WeenieError.MissileOutOfRange);

                // this prevents the accuracy bar from refilling when 'repeat attacks' is enabled
                Attacking = false;
                OnAttackDone();
                return;
            }

            var launchTime = EnqueueMotionPersist(actionChain, aimLevel);

            // launch projectile
            actionChain.AddAction(this, () =>
            {
                // handle self-procs
                TryProcEquippedItems(this, this, true, weapon);

                var sound = GetLaunchMissileSound(weapon);
                EnqueueBroadcast(new GameMessageSound(Guid, sound, 1.0f));

                // stamina usage
                // TODO: ensure enough stamina for attack
                // TODO: verify formulas - double/triple cost for bow/xbow?
                var staminaCost = GetAttackStamina(GetAccuracyRange());
                UpdateVitalDelta(Stamina, -staminaCost);

                var projectile = LaunchProjectile(launcher, ammo, target, origin, orientation, velocity);
                UpdateAmmoAfterLaunch(ammo);

                if (weapon != null && weapon.IsCleaving)
                {
                    var cleave = GetMissileCleaveTarget(creature, weapon);

                    foreach (var cleaveHit in cleave)
                    {
                        // target procs don't happen for cleaving
                        /*DamageTarget(cleaveHit, weapon);*/
                        /*LaunchCleaveMissile(cleaveHit, attackSequence, stance, subsequent = false);*/
                        var projectileSpeed = GetProjectileSpeed();
                        var aimVelocity = GetAimVelocity(cleaveHit, projectileSpeed);
                        var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);
                        var velocity = CalculateProjectileVelocity(localOrigin, cleaveHit, projectileSpeed, out Vector3 origin, out Quaternion orientation);

                        LaunchProjectile(launcher, ammo, cleaveHit, origin, orientation, velocity);
                        UpdateAmmoAfterLaunch(ammo);
                    }
                }

                if (DoMissileAoE)
                {
                    LaunchProjectile(launcher, ammo, target, origin, orientation, velocity);

                    // AoE Missile Attack
                    var aoeRoll = ThreadSafeRandom.Next(0.0f, 1.0f);

                    var aoeTarget = target as Creature;

                    if (aoeRoll <= MissileAoEChance && aoeTarget != null)
                    {
                        foreach (var m in GetMissileAoETarget(aoeTarget, weapon))
                        {
                            var projectileSpeed = GetProjectileSpeed();
                            var aimVelocity = GetAimVelocity(m, projectileSpeed);
                            var localOrigin = GetProjectileSpawnOrigin(ammo.WeenieClassId, aimLevel);
                            var velocity = CalculateProjectileVelocity(localOrigin, m, projectileSpeed, out Vector3 origin, out Quaternion orientation);

                            LaunchProjectile(launcher, ammo, m, origin, orientation, velocity);
                        }
                    }

                    DoMissileAoE = false;
                }
            });

            // ammo remaining?
            if (!ammo.UnlimitedUse && (ammo.StackSize == null || ammo.StackSize <= 1))
            {
                actionChain.AddAction(this, () =>
                {
                    Session.Network.EnqueueSend(new GameEventCommunicationTransientString(Session, "You are out of ammunition!"));
                    SetCombatMode(CombatMode.NonCombat);
                    Attacking = false;
                    OnAttackDone();
                });

                actionChain.EnqueueChain();
                return;
            }

            // reload animation
            var animSpeed = GetAnimSpeed();
            var reloadTime = EnqueueMotionPersist(actionChain, stance, MotionCommand.Reload, animSpeed);

            // reset for next projectile
            EnqueueMotionPersist(actionChain, stance, MotionCommand.Ready);
            var linkTime = MotionTable.GetAnimationLength(MotionTableId, stance, MotionCommand.Reload, MotionCommand.Ready);
            //var cycleTime = MotionTable.GetCycleLength(MotionTableId, CurrentMotionState.Stance, MotionCommand.Ready);

            actionChain.AddAction(this, () =>
            {
                if (CombatMode == CombatMode.Missile)
                    EnqueueBroadcast(new GameMessageParentEvent(this, ammo, ACE.Entity.Enum.ParentLocation.RightHand, ACE.Entity.Enum.Placement.RightHandCombat));
            });

            actionChain.AddDelaySeconds(linkTime);

            actionChain.AddAction(this, () =>
            {
                Attacking = false;

                if (creature.IsAlive && GetCharacterOption(CharacterOption.AutoRepeatAttacks) && !IsBusy && !AttackCancelled)
                {
                    // client starts refilling accuracy bar
                    Session.Network.EnqueueSend(new GameEventAttackDone(Session));

                    AccuracyLevel = AttackQueue.Fetch();

                    // can be cancelled, but cannot be pre-empted with another attack
                    var nextAttack = new ActionChain();
                    var nextRefillTime = AccuracyLevel;

                    NextRefillTime = DateTime.UtcNow.AddSeconds(nextRefillTime);
                    nextAttack.AddDelaySeconds(nextRefillTime);

                    // perform next attack
                    nextAttack.AddAction(this, () => { ValLaunchMissile(target, attackSequence, stance, true); });
                    nextAttack.EnqueueChain();

                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }
                }
                else
                {
                    if (DoBrutalizeAttack)
                    {
                        var currentUnixTime = Time.GetUnixTime();
                        DoBrutalizeAttack = false;
                        LastBrutalizeTimestamp = currentUnixTime;
                        PlayParticleEffect(PlayScript.EnchantDownRed, Guid);
                    }

                    OnAttackDone();
                }
            });

            actionChain.EnqueueChain();

            if (UnderLifestoneProtection)
                LifestoneProtectionDispel();
        }

        public void ValHandleActionUseItem(uint itemGuid)
        {
            if (PKLogout)
            {
                SendUseDoneEvent(WeenieError.YouHaveBeenInPKBattleTooRecently);
                return;
            }

            StopExistingMoveToChains();

            var item = FindObject(itemGuid, SearchLocations.MyInventory | SearchLocations.MyEquippedItems | SearchLocations.Landblock);

            if (IsTrading && ItemsInTradeWindow.Contains(item.Guid))
            {
                SendUseDoneEvent(WeenieError.TradeItemBeingTraded);
                //SendWeenieError(WeenieError.TradeItemBeingTraded);
                return;
            }

            if (item != null)
            {
                if (item.ItemType == ItemType.Portal)
                {
                    // kill pets
                    ActionChain killPets = new ActionChain();

                    killPets.AddAction(this, () =>
                    {
                        foreach (var monster in PhysicsObj.ObjMaint.GetVisibleObjectsValuesOfTypeCreature())
                        {
                            if (monster.IsCombatPet)
                            {
                                if (monster.PetOwner == Guid.Full)
                                {
                                    monster.Destroy();
                                    NumberOfPets--;
                                    if (NumberOfPets < 0)
                                    {
                                        NumberOfPets = 0;
                                    }
                                }
                            }
                        }
                    });

                    killPets.EnqueueChain();
                }

                // Ability Items
                if (item.IsAbilityItem)
                    DoAbility(this, item);

                if (item.CurrentLandblock != null && !item.Visibility && item.Guid != LastOpenedContainerId && !item.IsAbilityItem)
                {
                    if (IsBusy)
                    {
                        SendUseDoneEvent(WeenieError.YoureTooBusy);
                        return;
                    }

                    CreateMoveToChain(item, (success) => TryUseItem(item, success));
                }
                else
                    TryUseItem(item);
            }
            else
            {
                log.Debug($"{Name}.HandleActionUseItem({itemGuid:X8}): couldn't find object");
                SendUseDoneEvent();
            }
        }
    }
}

namespace ACE.Server.WorldObjects
{
    partial class WorldObject
    {
        /// <summary>
        /// Launches a targeted War Magic spell projectile
        /// </summary>
        protected void WarMagic(WorldObject target, Spell spell, WorldObject weapon, bool isWeaponSpell = false, bool fromProc = false)
        {
            CreateSpellProjectiles(spell, target, weapon, isWeaponSpell, fromProc);
        }
    }
}

namespace ACE.Server.WorldObjects
{
    partial class Creature
    {
        public static readonly float MissileCleaveAngle = 180.0f;
        public static readonly float MagicCleaveCylRange = 4.0f;
        public static readonly float MissileCleaveCylRange = 4.0f;
        public static readonly float MissileAoECylRange = 30.0f;
        public static readonly float MissileAoEAngle = 45.0f;
        public static readonly float DoTSpotAngle = 359;
        public static readonly float DoTSpotCylRange = 4;
        public static readonly float GunBladeProjectileSpeed = 300.0f;

        public List<Creature> GetDoTTarget(Creature target)
        {
            var player = this as Player;

            // sort visible objects by ascending distance
            var visible = PhysicsObj.ObjMaint.GetVisibleObjectsValuesWhere(o => o.WeenieObj.WorldObject != null);
            visible.Sort(DistanceComparator);

            var cleaveTargets = new List<Creature>();

            foreach (var obj in visible)
            {
                // only cleave creatures
                var creature = obj.WeenieObj.WorldObject as Creature;
                if (creature == null || creature.Teleporting || creature.IsDead) continue;

                if (player != null && player.CheckPKStatusVsTarget(creature, null) != null)
                    continue;

                if (!creature.Attackable && creature.TargetingTactic == TargetingTactic.None || creature.Teleporting)
                    continue;

                if (creature is CombatPet && (player != null || this is CombatPet))
                    continue;

                // no objects in cleave range
                var cylDist = GetCylinderDistance(creature);
                if (cylDist > DoTSpotCylRange)
                    return cleaveTargets;

                // only cleave in front of attacker
                var angle = GetAngle(creature);
                if (Math.Abs(angle) > DoTSpotAngle)
                    continue;

                // found cleavable object
                cleaveTargets.Add(creature);
                if (cleaveTargets.Count == 8)
                    break;
            }
            return cleaveTargets;
        }

        public float GetGunBladeProjectileSpeed()
        {
            var gunBlade = GetEquippedMeleeWeapon();

            var maxVelocity = gunBlade?.MaximumVelocity ?? GunBladeProjectileSpeed;

            if (maxVelocity == 0.0f)
            {
                // log.Warn($"{Name}.GetMissileSpeed() - {gunBlade.Name} ({gunBlade.Guid}) has speed 0");

                maxVelocity = GunBladeProjectileSpeed;
            }

            if (this is Player player && player.GetCharacterOption(CharacterOption.UseFastMissiles))
            {
                maxVelocity *= PropertyManager.GetDouble("fast_missile_modifier").Item;
            }

            // hard cap in physics engine
            maxVelocity = Math.Min(maxVelocity, PhysicsGlobals.MaxVelocity);

            //Console.WriteLine($"MaxVelocity: {maxVelocity}");

            return (float)maxVelocity;
        }

        public virtual bool ValFindNextTarget()
        {
            stopwatch.Restart();

            try
            {
                SelectTargetingTactic();
                SetNextTargetTime();

                var visibleTargets = GetAttackTargets();
                if (visibleTargets.Count == 0)
                {
                    if (MonsterState != State.Return)
                        MoveToHome();

                    return false;
                }

                // Generally, a creature chooses whom to attack based on:
                //  - who it was last attacking,
                //  - who attacked it last,
                //  - or who caused it damage last.

                // When players first enter the creature's detection radius, however, none of these things are useful yet,
                // so the creature chooses a target randomly, weighted by distance.

                // Players within the creature's detection sphere are weighted by how close they are to the creature --
                // the closer you are, the more chance you have to be selected to be attacked.

                var prevAttackTarget = AttackTarget;

                switch (CurrentTargetingTactic)
                {
                    case TargetingTactic.None:

                        Console.WriteLine($"{Name}.FindNextTarget(): TargetingTactic.None");
                        break; // same as focused?

                    case TargetingTactic.Random:

                        // this is a very common tactic with monsters,
                        // although it is not truly random, it is weighted by distance
                        var targetDistances = BuildTargetDistance(visibleTargets);
                        AttackTarget = SelectWeightedDistance(targetDistances);
                        break;

                    case TargetingTactic.Focused:

                        break; // always stick with original target?

                    case TargetingTactic.LastDamager:

                        var lastDamager = DamageHistory.LastDamager?.TryGetAttacker() as Creature;
                        if (lastDamager != null && lastDamager is Player || lastDamager != null && lastDamager.IsCombatPet)
                            AttackTarget = lastDamager;
                        else
                        {
                            var newTargetDistances = BuildTargetDistance(visibleTargets);
                            AttackTarget = SelectWeightedDistance(newTargetDistances);
                        }
                        break;

                    case TargetingTactic.TopDamager:

                        var topDamager = DamageHistory.TopDamager?.TryGetAttacker() as Creature;
                        if (topDamager != null && topDamager is Player || topDamager != null && topDamager.IsCombatPet)
                            AttackTarget = topDamager;
                        else
                        {
                            var nearest = BuildTargetDistance(visibleTargets);
                            AttackTarget = nearest[0].Target;
                        }
                        break;

                    // these below don't seem to be used in PY16 yet...

                    case TargetingTactic.Weakest:

                        // should probably shuffle the list beforehand,
                        // in case a bunch of levels of same level are in a group,
                        // so the same player isn't always selected
                        var lowestLevel = visibleTargets.OrderBy(p => p.Level).FirstOrDefault();
                        AttackTarget = lowestLevel;
                        break;

                    case TargetingTactic.Strongest:

                        var highestLevel = visibleTargets.OrderByDescending(p => p.Level).FirstOrDefault();
                        AttackTarget = highestLevel;
                        break;

                    case TargetingTactic.Nearest:

                        var nearest1 = BuildTargetDistance(visibleTargets);
                        AttackTarget = nearest1[0].Target;
                        break;

                    case TargetingTactic.HasShield:

                        var hasShieldIsTank = visibleTargets.Where(p => p.GetEquippedShield() != null && IsTank).ToList();
                        var noShieldIsTank = visibleTargets.Where(p => p.GetEquippedShield() == null && IsTank).ToList();
                        var hasShield = visibleTargets.Where(p => p.GetEquippedShield() != null).ToList();
                        if (hasShieldIsTank.Count > 0)
                        {
                            var shieldDistances = BuildTargetDistance(hasShieldIsTank);
                            AttackTarget = SelectWeightedDistance(shieldDistances);
                        }
                        else if (noShieldIsTank.Count > 0)
                        {
                            var noShieldDistances = BuildTargetDistance(noShieldIsTank);
                            AttackTarget = SelectWeightedDistance(noShieldDistances);
                        }
                        else if (hasShield.Count > 0)
                        {
                            var shieldDistances = BuildTargetDistance(hasShield);
                            AttackTarget = SelectWeightedDistance(shieldDistances);
                        }
                        else if (hasShieldIsTank.Count == 0 && hasShield.Count == 0 && noShieldIsTank.Count == 0)
                        {
                            var topDamager1 = DamageHistory.TopDamager?.TryGetAttacker() as Creature;
                            if (topDamager1 != null && topDamager1 is Player || topDamager1 != null && topDamager1.IsCombatPet)
                                AttackTarget = topDamager1;
                            else
                            {
                                var nearest2 = BuildTargetDistance(visibleTargets);
                                AttackTarget = nearest2[0].Target;
                            }
                        }
                        break;

                    case TargetingTactic.HighestThreat:

                        var highestThreat = DamageHistory.HighestThreat?.TryGetAttacker() as Creature;
                        var topThreatDamager = DamageHistory.TopDamager?.TryGetAttacker() as Creature;
                        if (DamageHistory.HighestThreat != null && DamageHistory.TopDamager != null)
                        {
                            if (DamageHistory.HighestThreat.TotalThreat > DamageHistory.TopDamager.TotalDamage)
                                AttackTarget = highestThreat;
                            else
                                AttackTarget = topThreatDamager;
                        }
                        else
                        {
                            var nearestThreat = BuildTargetDistance(visibleTargets);
                            AttackTarget = nearestThreat[0].Target;
                        }
                        break;
                }

                //Console.WriteLine($"{Name}.FindNextTarget = {AttackTarget.Name}");

                if (AttackTarget != null && AttackTarget != prevAttackTarget)
                    EmoteManager.OnNewEnemy(AttackTarget);

                return AttackTarget != null;
            }
            finally
            {
                ServerPerformanceMonitor.AddToCumulativeEvent(ServerPerformanceMonitor.CumulativeEventHistoryType.Monster_Awareness_FindNextTarget, stopwatch.Elapsed.TotalSeconds);
            }
        }
    }
}

namespace ACE.Server.Entity
{
    public partial class DamageHistoryInfo
    {
        public readonly int DoTOwnerGuid;
        public float TotalThreat;

        public DamageHistoryInfo(WorldObject attacker, bool valVersion, float totalDamage = 0.0f)
        {
            Attacker = new WeakReference<WorldObject>(attacker);

            Guid = attacker.Guid;
            Name = attacker.Name;
            DoTOwnerGuid = attacker.DoTOwnerGuid;

            TotalDamage = totalDamage;
            TotalThreat = totalDamage;

            var tankTotalThreatMod = 5.0f;

            if (attacker is Player player)
            {
                if (player.IsTank)
                {
                    if (player.TauntTimerActive)
                        tankTotalThreatMod = 10.0f;

                    TotalThreat *= tankTotalThreatMod;
                }
                else
                    TotalThreat *= totalDamage * 0.5f;
            }

            if (attacker is CombatPet combatPet && combatPet.P_PetOwner != null)
                PetOwner = new WeakReference<Player>(combatPet.P_PetOwner);

            if (attacker.WeenieClassId == 300501)
            {
                foreach (var p in PlayerManager.GetAllOnline())
                {
                    if (p.Guid.Full == attacker.DoTOwnerGuid)
                    {
                        if (DoTOwnerGuid != 0)
                        {
                            Guid = p.Guid;
                            Name = p.Name;
                        }
                    }
                }
            }
        }

    }
}


namespace ACE.Server.Entity
{
    public partial class DamageHistory
    {
        public readonly Dictionary<ObjectGuid, DamageHistoryInfo> TotalThreat = new Dictionary<ObjectGuid, DamageHistoryInfo>();

        public DamageHistoryInfo HighestThreat => GetHighestThreat();

        public DamageHistoryInfo GetHighestThreat(bool includeSelf = true)
        {
            var sorted = TotalThreat.Values.Where(wo => includeSelf || wo.Guid != Creature.Guid).OrderByDescending(wo => wo.TotalThreat);

            return sorted.FirstOrDefault();
        }
    }
}

public static class ValExtensions
{

}
