export interface CustomerRequest {
    rid: string;
    customerId: string;
    propertyId: string;
    executiveId: string | null;
    locality: string;
    requestStatus: string;
    executiveDetails?: Executive; // Optional field to hold executive details if available
  }
  
  export interface Executive {
    name: string;
    email: string;
    number: string;
  }