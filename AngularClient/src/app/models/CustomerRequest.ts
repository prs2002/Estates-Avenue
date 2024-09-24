export interface CustomerRequest {
    rid: string;
    customerId: string;
    propertyId: string;
    executiveId: string | null;
    locality: string;
    requestStatus: string;
    // customerDetails?: Customer;  //  field to store fetched customer name
    // propertyDetails?: Property;  //  field to store fetched property name
    executiveDetails?: Executive; // Optional field to hold executive details if assigned
  }
  
  export interface Executive {
    name: string;
    email: string;
    number: string;
  }

  // export interface Customer {
  //   id: string;
  //   name: string;
  // }
  
  // export interface Property {
  //   id: string;
  //   name: string;
  // }
  