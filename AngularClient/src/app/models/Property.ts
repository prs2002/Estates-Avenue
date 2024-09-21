export interface Property {
  pid: number;
  name: string;
  location: string;
  rate: number;
  propertyType: string;
  desc: string;
  executiveId: string;
  customerId: string;
  imageUrl: string; // New field for image URL
}