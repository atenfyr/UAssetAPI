using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.FieldTypes
{
    /// <summary>Secondary condition to check before considering the replication of a lifetime property.</summary>
    public enum ELifetimeCondition : byte
    {
        /// <summary>This property has no condition, and will send anytime it changes</summary>
        COND_None = 0,
        /// <summary>This property will only attempt to send on the initial bunch</summary>
        COND_InitialOnly = 1,
        /// <summary>This property will only send to the actor's owner</summary>
        COND_OwnerOnly = 2,
        /// <summary>This property send to every connection EXCEPT the owner</summary>
        COND_SkipOwner = 3,
        /// <summary>This property will only send to simulated actors</summary>
        COND_SimulatedOnly = 4,
        /// <summary>This property will only send to autonomous actors</summary>
        COND_AutonomousOnly = 5,
        /// <summary>This property will send to simulated OR bRepPhysics actors</summary>
        COND_SimulatedOrPhysics = 6,
        /// <summary>This property will send on the initial packet, or to the actors owner</summary>
        COND_InitialOrOwner = 7,
        /// <summary>This property has no particular condition, but wants the ability to toggle on/off via SetCustomIsActiveOverride</summary>
        COND_Custom = 8,
        /// <summary>This property will only send to the replay connection, or to the actors owner</summary>
        COND_ReplayOrOwner = 9,
        /// <summary>This property will only send to the replay connection</summary>
        COND_ReplayOnly = 10,
        /// <summary>This property will send to actors only, but not to replay connections</summary>
        COND_SimulatedOnlyNoReplay = 11,
        /// <summary>This property will send to simulated Or bRepPhysics actors, but not to replay connections</summary>
        COND_SimulatedOrPhysicsNoReplay = 12,
        /// <summary>This property will not send to the replay connection</summary>
        COND_SkipReplay = 13,
        /// <summary>This property will never be replicated</summary>
        COND_Never = 15,
        COND_Max = 16
    };
}
