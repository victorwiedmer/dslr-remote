using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canon_EOS_Remote.classes
{
    class EventCodes
    {
        private List<TEventID> eventIDs;

        internal List<TEventID> EventIDs
        {
            get { return eventIDs; }
            set { eventIDs = value; }
        }

        public EventCodes()
        {
            this.EventIDs = new List<TEventID>();
            init();
        }

        private void init()
        {
        this.EventIDs.Add(new TEventID("PropertyEvent_All", 0x00000100));
        this.EventIDs.Add(new TEventID("PropertyEvent_PropertyChanged", 0x00000101));
        this.EventIDs.Add(new TEventID("PropertyEvent_PropertyDescChanged", 0x00000102));
        this.EventIDs.Add(new TEventID("ObjectEvent_All", 0x00000200));
        this.EventIDs.Add(new TEventID("ObjectEvent_VolumeInfoChanged",0x00000201));
        this.EventIDs.Add(new TEventID("ObjectEvent_VolumeUpdateItems",0x00000202));
        this.EventIDs.Add(new TEventID("ObjectEvent_FolderUpdateItems",0x00000203));
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemCreated",0x00000204));
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemRemoved",0x00000205));
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemInfoChanged",0x00000206));
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemContentChanged",0x00000207));
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemRequestTransfer",0x00000208));
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemRequestTransferDT",0x00000209));     
        this.EventIDs.Add(new TEventID("ObjectEvent_DirItemCancelTransferDT",0x0000020a));
        this.EventIDs.Add(new TEventID("ObjectEvent_VolumeAdded",0x0000020c));
		this.EventIDs.Add(new TEventID("ObjectEvent_VolumeRemoved",0x0000020d));
        this.EventIDs.Add(new TEventID("StateEvent_All",0x00000300));
        this.EventIDs.Add(new TEventID("StateEvent_Shutdown",0x00000301));
        this.EventIDs.Add(new TEventID("StateEvent_JobStatusChanged",0x00000302));
        this.EventIDs.Add(new TEventID("StateEvent_WillSoonShutDown",0x00000303));
        this.EventIDs.Add(new TEventID("StateEvent_ShutDownTimerUpdate",0x00000304)); 
        this.EventIDs.Add(new TEventID("StateEvent_CaptureError",0x00000305));
        this.EventIDs.Add(new TEventID("StateEvent_InternalError",0x00000306));
        this.EventIDs.Add(new TEventID("StateEvent_AfResult",0x00000309));
		this.EventIDs.Add(new TEventID("StateEvent_BulbExposureTime",0x00000310));	
        }

        public string getEventIDString(UInt32 eventIDCode)
        {
            for (int i = 0; i < this.EventIDs.Count; i++)
            {
                if (this.EventIDs.ElementAt(i).EventIDCode == eventIDCode)
                {
                    return this.EventIDs.ElementAt(i).EventIDString;
                }
            }
            return "unknown";
        }
    }
}
