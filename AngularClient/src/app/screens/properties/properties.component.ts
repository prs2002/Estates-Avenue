import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/property.service';
import { Property } from '../../models/Property';
import { faFilter, faMagnifyingGlass, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent implements OnInit {

  heading:string = "List of Properties";
  plusicon = faPlusCircle;
  searchicon = faMagnifyingGlass;
  filtericon = faFilter;
  add:string = "New Property"
  propdesc:string = "Find properties for rent by selecting from the curated list of available properties, or by entering your search criteria below. If you have any queries feel free to contact, will help you find the perfect rental property!"
  propoverlaytext1:string = "Properties For Rent"
  propoverlaytext2:string = "Explore diverse range of rental properties tailored to your needs. Whether it's a cozy residential retreat, a bustling commercial space, or an industrial hub, find the perfect rental to fuel your aspirations. Unlock endless possibilities with curated selection of properties for rent."
  isPropertyFormVisible: boolean = false;

  properties: Property[] = [];
  filteredProperties: Property[] = [];
  searchTerm: string = '';
  newProperty: Property = {
    pid: 0,
    name: '',
    location: '',
    rate: 0,
    propertyType: '',
    desc: '',
    executiveId: '',
    customerId: '',
    imageUrl: '',
  };
  selectedImage: File | null = null; // To hold the image file
  isManager: boolean = false;

  constructor(private propertyService: PropertyService,private authService: AuthService) { }

  ngOnInit(): void {
    this.fetchProperties();
    this.checkUserRole();
  }

  fetchProperties(): void {
    this.propertyService.getProperties().subscribe(data => {
      this.properties = data.map(property => ({
        ...property,
        imageUrl: `assets/images/${property.imageUrl}` // Construct the image URL
    }));
      this.filteredProperties = data; // Initially display all properties
    });
  }
  openPropertyForm(): void {
    this.isPropertyFormVisible = !this.isPropertyFormVisible; // Toggle form visibility
  }

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedImage = input.files[0];
    }
  }
 
  // createProperty(): void {
  //   this.propertyService.createProperty(this.newProperty,this.newProperty.imageUrl).subscribe(response => {
  //       this.fetchProperties(); // Refresh property list
  //       this.isPropertyFormVisible = false; // Hide form after creation
  //   });
  // }
  createProperty(): void {
    if (this.selectedImage) {
      this.propertyService.createProperty(this.newProperty, this.newProperty.imageUrl).subscribe(response => {
        this.fetchProperties(); // Refresh property list
        this.isPropertyFormVisible = false; // Hide form after creation
        // Reset newProperty if needed
        this.newProperty = {
          pid: 0,
          name: '',
          location: '',
          rate: 0,
          propertyType: '',
          desc: '',
          executiveId: '',
          customerId: '',
          imageUrl: ''
        };
        this.selectedImage = null; // Reset image selection
      });
    }
  }

  filterItems(): void {
    this.filteredProperties = this.properties.filter(property =>
      property.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
    // Check if the user is a manager
    checkUserRole(): void {
      const role = this.authService.getUserRole();
      if (role === 'manager') {
        this.isManager = true;
      }
      else{
      }
    }
  showFilters: boolean = false;
  selectedCategories: string[] = [];
  
  toggleFilters() {
    this.showFilters = !this.showFilters;
  }
  onCheckboxChange(category: string) {
  }
  isCategoryChecked(category: string): boolean {
    return false;
  }
  applyFilters() {
  }   
  cancelFilters() {
  }

}