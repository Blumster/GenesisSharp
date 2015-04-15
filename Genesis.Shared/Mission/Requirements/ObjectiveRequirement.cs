using System;
using System.Xml.Linq;

namespace Genesis.Shared.Mission.Requirements
{
    using Constant;

    public abstract class ObjectiveRequirement
    {
        public MissionObjective ObjectiveOwner;
        public RequirementType RequirementType;
        public Byte FirstStateSlot;

        protected ObjectiveRequirement(MissionObjective owner)
        {
            ObjectiveOwner = owner;
        }

        public abstract void UnSerialize(XElement elem);

        public static ObjectiveRequirement Create(MissionObjective owner, XElement elem)
        {
            var type = (String)elem.Attribute("type");

            ObjectiveRequirement req;

            switch (type)
            {
                case "kill":
                    req = new ObjectiveRequirementKill(owner);
                    break;

                case "kill_aggregate":
                    req = new ObjectiveRequirementKillAggregate(owner);
                    break;

                case "collect":
                    req = new ObjectiveRequirementCollect(owner);
                    break;

                case "deliver":
                    req = new ObjectiveRequirementDeliver(owner);
                    break;

                case "money":
                    req = new ObjectiveRequirementMoney(owner);
                    break;

                case "stunt":
                    req = new ObjectiveRequirementStunt(owner);
                    break;

                case "mission":
                    req = new ObjectiveRequirementMission(owner);
                    break;

                case "km":
                    req = new ObjectiveRequirementKm(owner);
                    break;

                case "timeplayed":
                    req = new ObjectiveRequirementTimePlayed(owner);
                    break;

                case "patrol":
                    req = new ObjectiveRequirementPatrol(owner);
                    break;

                case "useitem":
                    req = new ObjectiveRequirementUseItem(owner);
                    break;

                case "characterlevel":
                    req = new ObjectiveRequirementCharacterLevel(owner);
                    break;

                case "escort":
                    req = new ObjectiveRequirementEscort(owner);
                    break;

                case "crazytaxi":
                    req = new ObjectiveRequirementCrazyTaxi(owner);
                    break;

                default:
                    return null;
            }

            req.UnSerialize(elem);

            return req;
        }
    }
}
