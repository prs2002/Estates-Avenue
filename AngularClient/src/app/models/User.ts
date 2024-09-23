export interface User {
    name: string;
    email: string;
    password: string;
    number: string;
    location: string;
    userType: string; // This will be set to "user" by default
  }

  export interface Executive {
    id: string;
    name: string;
    email: string;
    number: string;
  }
  