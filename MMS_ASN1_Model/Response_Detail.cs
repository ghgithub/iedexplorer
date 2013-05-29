
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
    [ASN1Choice ( Name = "Response_Detail") ]
    public class Response_Detail : IASN1PreparedElement {
                    
        
	private NullObject otherRequests_ ;
        private bool  otherRequests_selected = false ;
        
                
        
        [ASN1Null ( Name = "otherRequests" )]
    
        [ASN1Element ( Name = "otherRequests", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public NullObject OtherRequests
        {
            get { return otherRequests_; }
            set { selectOtherRequests(value); }
        }
        
                
          
        
	private CS_Status_Response status_ ;
        private bool  status_selected = false ;
        
                
        
        [ASN1Element ( Name = "status", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public CS_Status_Response Status
        {
            get { return status_; }
            set { selectStatus(value); }
        }
        
                
          
        
	private CS_GetProgramInvocationAttributes_Response getProgramInvocationAttributes_ ;
        private bool  getProgramInvocationAttributes_selected = false ;
        
                
        
        [ASN1Element ( Name = "getProgramInvocationAttributes", IsOptional =  false , HasTag =  true, Tag = 45 , HasDefaultValue =  false )  ]
    
        public CS_GetProgramInvocationAttributes_Response GetProgramInvocationAttributes
        {
            get { return getProgramInvocationAttributes_; }
            set { selectGetProgramInvocationAttributes(value); }
        }
        
                
          
        
	private CS_GetEventConditionAttributes_Response getEventConditionAttributes_ ;
        private bool  getEventConditionAttributes_selected = false ;
        
                
        
        [ASN1Element ( Name = "getEventConditionAttributes", IsOptional =  false , HasTag =  true, Tag = 49 , HasDefaultValue =  false )  ]
    
        public CS_GetEventConditionAttributes_Response GetEventConditionAttributes
        {
            get { return getEventConditionAttributes_; }
            set { selectGetEventConditionAttributes(value); }
        }
        
                
          
        public bool isOtherRequestsSelected () {
            return this.otherRequests_selected ;
        }

        
        public void selectOtherRequests () {
            selectOtherRequests (new NullObject());
	}
	


        public void selectOtherRequests (NullObject val) {
            this.otherRequests_ = val;
            this.otherRequests_selected = true;
            
            
                    this.status_selected = false;
                
                    this.getProgramInvocationAttributes_selected = false;
                
                    this.getEventConditionAttributes_selected = false;
                            
        }
        
          
        public bool isStatusSelected () {
            return this.status_selected ;
        }

        


        public void selectStatus (CS_Status_Response val) {
            this.status_ = val;
            this.status_selected = true;
            
            
                    this.otherRequests_selected = false;
                
                    this.getProgramInvocationAttributes_selected = false;
                
                    this.getEventConditionAttributes_selected = false;
                            
        }
        
          
        public bool isGetProgramInvocationAttributesSelected () {
            return this.getProgramInvocationAttributes_selected ;
        }

        


        public void selectGetProgramInvocationAttributes (CS_GetProgramInvocationAttributes_Response val) {
            this.getProgramInvocationAttributes_ = val;
            this.getProgramInvocationAttributes_selected = true;
            
            
                    this.otherRequests_selected = false;
                
                    this.status_selected = false;
                
                    this.getEventConditionAttributes_selected = false;
                            
        }
        
          
        public bool isGetEventConditionAttributesSelected () {
            return this.getEventConditionAttributes_selected ;
        }

        


        public void selectGetEventConditionAttributes (CS_GetEventConditionAttributes_Response val) {
            this.getEventConditionAttributes_ = val;
            this.getEventConditionAttributes_selected = true;
            
            
                    this.otherRequests_selected = false;
                
                    this.status_selected = false;
                
                    this.getProgramInvocationAttributes_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Response_Detail));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
            
}