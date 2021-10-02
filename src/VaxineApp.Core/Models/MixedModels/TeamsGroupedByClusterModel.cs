using System.Collections.Generic;

namespace VaxineApp.Core.Models.MixedModels
{
    public class TeamsGroupedByClusterModel : List<TeamModel>
    {
        public string ClusterName { get; private set; }
        public TeamsGroupedByClusterModel(string clusterName, List<TeamModel> Teams) : base(Teams)
        {
            this.ClusterName = clusterName;
        }
    }
}
