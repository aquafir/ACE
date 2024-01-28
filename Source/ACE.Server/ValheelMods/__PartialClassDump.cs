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

namespace ACE.Server.WorldObjects
{
    partial class Player
    {
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

    }
}
