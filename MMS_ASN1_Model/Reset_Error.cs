
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
    [ASN1BoxedType ( Name = "Reset_Error") ]
    public class Reset_Error: IASN1PreparedElement {
            
           
        private ProgramInvocationState  val;

        
        [ASN1Element ( Name = "Reset-Error", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ProgramInvocationState Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public Reset_Error ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Reset_Error));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}