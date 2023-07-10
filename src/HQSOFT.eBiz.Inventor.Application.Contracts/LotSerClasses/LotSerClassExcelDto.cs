using HQSOFT.eBiz.Inventor.Lots;
using System;

namespace HQSOFT.eBiz.Inventor.LotSerClasses
{
    public class LotSerClassExcelDto
    {
        public string ClassID { get; set; }
        public string Description { get; set; }
        public TrackingMethod TrackingMethod { get; set; }
        public bool TrackExpriationDate { get; set; }
        public bool RequiredforDropShip { get; set; }
        public AssignMethod AssignMethod { get; set; }
        public IssueMethod IssueMethod { get; set; }
        public bool ShareAutoIncremenitalValueBetwenAllClassItems { get; set; }
        public int AutoIncremetalValue { get; set; }
        public bool AutoGenerateNextNumber { get; set; }
        public int MaxAutoNumbers { get; set; }
    }
}