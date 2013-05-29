
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
    [ASN1Sequence ( Name = "AlterEventEnrollment_Response", IsSet = false  )]
    public class AlterEventEnrollment_Response : IASN1PreparedElement {
                    
	private CurrentStateChoiceType currentState_ ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "currentState" )]
    public class CurrentStateChoiceType : IASN1PreparedElement  {
	            
        
	private EE_State state_ ;
        private bool  state_selected = false ;
        
                
        
        [ASN1Element ( Name = "state", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public EE_State State
        {
            get { return state_; }
            set { selectState(value); }
        }
        
                
          
        
	private NullObject undefined_ ;
        private bool  undefined_selected = false ;
        
                
        
        [ASN1Null ( Name = "undefined" )]
    
        [ASN1Element ( Name = "undefined", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public NullObject Undefined
        {
            get { return undefined_; }
            set { selectUndefined(value); }
        }
        
                
          
        public bool isStateSelected () {
            return this.state_selected ;
        }

        


        public void selectState (EE_State val) {
            this.state_ = val;
            this.state_selected = true;
            
            
                    this.undefined_selected = false;
                            
        }
        
          
        public bool isUndefinedSelected () {
            return this.undefined_selected ;
        }

        
        public void selectUndefined () {
            selectUndefined (new NullObject());
	}
	


        public void selectUndefined (NullObject val) {
            this.undefined_ = val;
            this.undefined_selected = true;
            
            
                    this.state_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CurrentStateChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "currentState", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public CurrentStateChoiceType CurrentState
        {
            get { return currentState_; }
            set { currentState_ = value;  }
        }
        
                
          
	private EventTime transitionTime_ ;
	
        [ASN1Element ( Name = "transitionTime", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public EventTime TransitionTime
        {
            get { return transitionTime_; }
            set { transitionTime_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(AlterEventEnrollment_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}