import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { faFilter, faMagnifyingGlass, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { Property } from '../../models/Property';
import { AuthService } from 'src/app/services/auth.service';
import { PropertyService } from '../../services/property.service';
import { CustReqService } from 'src/app/services/cust-req.service';

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent implements OnInit {

  customerId: string; // Assume you have this from user authentication
  heading:string = "List of Properties";
  plusicon = faPlusCircle;
  searchicon = faMagnifyingGlass;
  filtericon = faFilter;
  add:string = "New Property"
  propdesc:string = "Find properties for rent by selecting from the curated list of available properties, or by entering your search criteria below. If you have any queries feel free to contact, will help you find the perfect rental property!"
  propoverlaytext1:string = "Properties For Rent"
  propoverlaytext2:string = "Explore diverse range of rental properties tailored to your needs. Whether it's a cozy residential retreat, a bustling commercial space, or an industrial hub, find the perfect rental to fuel your aspirations. Unlock endless possibilities with curated selection of properties for rent."
  isPropertyFormVisible: boolean = false;
  isEditFormVisible: boolean = false;

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
  editableProperty: Property | null = null; // Hold the property to be edited

  constructor(private propertyService: PropertyService, private authService: AuthService,
    private reqService: CustReqService, private http: HttpClient, private router: Router){
    this.customerId = this.authService.getCustomerId()?? '';  // Fetch the customerId from AuthService
  }
  addToWishlist(property: any) {
    const customerRequest = {
      customerId: this.customerId,          // Current user's ID
      propertyId: property.pid.toString(),             // ID of the clicked property
      locality: property.location,          // Location of the property
      requestStatus: 'pending'              // Set status as 'pending'
    };
    this.reqService.addToWishlist(customerRequest).subscribe({
      next: (response) => {
        if (response.message === "Property added to wishlist successfully.") {
          console.log(response);
          alert('Property added to your wishlist!');
          this.router.navigate(['/wishlist']);
        }
      },
      error: (err) => {
        if (err.status === 409) {
          alert('This property is already in your wishlist.');
        } else {
          console.error('Error adding to wishlist', err);
          alert('Failed to add property to wishlist. Please try again.');
        }
      }
    });
  }
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
  editProperty(property: Property): void {
    this.editableProperty = { ...property }; // Create a copy of the selected property
    this.isEditFormVisible = true; // Show the form
  }
 
  cancelEdit(): void {
    this.editableProperty = null;
    this.isEditFormVisible = false;
  }

  // Update the property with new details
  updateProperty(): void {
    if (this.editableProperty) {
      this.propertyService.updateProperty(this.editableProperty).subscribe(response => {
        this.fetchProperties(); // Refresh property list after update
        this.isEditFormVisible = false; // Hide the form
        this.editableProperty = null; // Reset editable property
      });
    }
  }
  deleteProperty(propertyId: number): void {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.deleteProperty(propertyId).subscribe(
        () => {
          this.fetchProperties(); // Refresh the list after deletion
        },
        error => {
          console.error('Error deleting property:', error);
        }
      );
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