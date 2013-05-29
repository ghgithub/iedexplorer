
//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System;
using org.bn.attributes;
using org.bn.attributes.constraints;
using org.bn.coders;
using org.bn.types;
using org.bn;

namespace MMS_ASN1_Model {


    [ASN1PreparedElement]
    [ASN1Sequence ( Name = "ReportEventConditionStatus_Response", IsSet = false  )]
    public class ReportEventConditionStatus_Response : IASN1PreparedElement {
                    
	private EC_State currentState_ ;
	
        [ASN1Element ( Name = "currentState", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public EC_State CurrentState
        {
            get { return currentState_; }
            set { currentState_ = value;  }
        }
        
                
          
	private Unsigned32 numberOfEventEnrollments_ ;
	
        [ASN1Element ( Name = "numberOfEventEnrollments", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Unsigned32 NumberOfEventEnrollments
        {
            get { return numberOfEventEnrollments_; }
            set { numberOfEventEnrollments_ = value;  }
        }
        
                
          
	private bool enabled_ ;
	
        private bool  enabled_present = false ;
	[ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "enabled", IsOptional =  true , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public bool Enabled
        {
            get { return enabled_; }
            set { enabled_ = value; enabled_present = true;  }
        }
        
                
          
	private EventTime timeOfLastTransitionToActive_ ;
	
        private bool  timeOfLastTransitionToActive_present = false ;
	
        [ASN1Element ( Name = "timeOfLastTransitionToActive", IsOptional =  true , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public EventTime TimeOfLastTransitionToActive
        {
            get { return timeOfLastTransitionToActive_; }
            set { timeOfLastTransitionToActive_ = value; timeOfLastTransitionToActive_present = true;  }
        }
        
                
          
	private EventTime timeOfLastTransitionToIdle_ ;
	
        private bool  timeOfLastTransitionToIdle_present = false ;
	
        [ASN1Element ( Name = "timeOfLastTransitionToIdle", IsOptional =  true , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public EventTime TimeOfLastTransitionToIdle
        {
            get { return timeOfLastTransitionToIdle_; }
            set { timeOfLastTransitionToIdle_ = value; timeOfLastTransitionToIdle_present = true;  }
        }
        
                
  
        public bool isEnabledPresent () {
            return this.enabled_present == true;
        }
        
        public bool isTimeOfLastTransitionToActivePresent () {
            return this.timeOfLastTransitionToActive_present == true;
        }
        
        public bool isTimeOfLastTransitionToIdlePresent () {
            return this.timeOfLastTransitionToIdle_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ReportEventConditionStatus_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}