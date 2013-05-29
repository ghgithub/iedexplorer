
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
    [ASN1Sequence ( Name = "GetNamedTypeAttributes_Response", IsSet = false  )]
    public class GetNamedTypeAttributes_Response : IASN1PreparedElement {
                    
	private bool mmsDeletable_ ;
	[ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "mmsDeletable", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public bool MmsDeletable
        {
            get { return mmsDeletable_; }
            set { mmsDeletable_ = value;  }
        }
        
                
          
	private TypeSpecification typeSpecification_ ;
	
        [ASN1Element ( Name = "typeSpecification", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public TypeSpecification TypeSpecification
        {
            get { return typeSpecification_; }
            set { typeSpecification_ = value;  }
        }
        
                
          
	private Identifier accessControlList_ ;
	
        private bool  accessControlList_present = false ;
	
        [ASN1Element ( Name = "accessControlList", IsOptional =  true , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Identifier AccessControlList
        {
            get { return accessControlList_; }
            set { accessControlList_ = value; accessControlList_present = true;  }
        }
        
                
          
	private string meaning_ ;
	
        private bool  meaning_present = false ;
	[ASN1String( Name = "", 
        StringType =  UniversalTags.VisibleString , IsUCS = false )]
        [ASN1Element ( Name = "meaning", IsOptional =  true , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public string Meaning
        {
            get { return meaning_; }
            set { meaning_ = value; meaning_present = true;  }
        }
        
                
  
        public bool isAccessControlListPresent () {
            return this.accessControlList_present == true;
        }
        
        public bool isMeaningPresent () {
            return this.meaning_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GetNamedTypeAttributes_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}