# BBQ and Grill - Refactoring Guide

## Quick Wins Completed ✅

### 1. Security Improvements
- ✅ Removed hardcoded credentials from Web.config
- ✅ Added environment variable support
- ✅ Enabled custom error pages for production
- ✅ Added security headers (X-Content-Type-Options, X-Frame-Options, X-XSS-Protection)
- ✅ Configured HTTPS redirect

### 2. Dependency Updates
- ✅ Updated jQuery from 1.2.6 (2008) to 3.7.1 (latest)
- ✅ Updated Bootstrap from 3.3.6 to 5.3.2 (latest)
- ✅ Changed all CDN links to HTTPS

### 3. Code Organization
- ✅ Created `ConfigurationHelper` for centralized configuration management
- ✅ Created `EmailService` to extract email logic from code-behind
- ✅ Created `DatabaseService` for proper connection management
- ✅ Refactored `ContactUs.aspx.cs` to use service layer
- ✅ Refactored `Locations.aspx.cs` to use service layer with proper disposal

### 4. Configuration Management
- ✅ Added `.env.example` for environment variable documentation
- ✅ Created `Web.config.local` for local development
- ✅ Updated `.gitignore` to exclude sensitive configuration files

## Setup Instructions

### For Local Development

1. **Copy the local configuration:**
   ```bash
   copy Web.config.local Web.config
   ```

2. **Update credentials in Web.config** (for local dev only)
   - Update database connection string
   - Update SMTP settings

### For Production Deployment

1. **Set environment variables:**
   ```bash
   # Database
   BBQ_DB_CONNECTION_STRING=Data Source=your-server;Initial Catalog=your-db;User ID=user;Password=pass

   # SMTP
   SMTP_HOST=smtp.gmail.com
   SMTP_PORT=587
   SMTP_USERNAME=your-email@gmail.com
   SMTP_PASSWORD=your-app-password
   SMTP_FROM_EMAIL=your-email@gmail.com
   SMTP_ENABLE_SSL=true
   ```

2. **Deploy Web.config** (without credentials)
   - The production Web.config has empty values
   - Configuration is loaded from environment variables

## Next Steps - Long-term Roadmap

### Phase 2: Extract Business Logic ✅ COMPLETED
- ✅ Create `LocationService` for location search logic
- ✅ Create Models for data entities (Location, MenuItem, ContactMessage)
- ✅ Move business logic out of code-behind files
- [ ] Create `MenuService` for menu operations (if needed)
- [ ] Refactor remaining pages to use service layer

### Phase 3: Implement Data Access Layer
- [ ] Create Models folder with entity classes
- [ ] Create Repositories folder
- [ ] Implement Repository pattern for data access
- [ ] Consider Entity Framework or Dapper

### Phase 4: Add Logging and Monitoring
- [ ] Install NLog or Serilog
- [ ] Add logging to all services
- [ ] Create error logging middleware
- [ ] Add performance monitoring

### Phase 5: Migration to ASP.NET Core
- [ ] Create new ASP.NET Core 8 project
- [ ] Migrate models and business logic
- [ ] Convert Web Forms to Razor Pages or MVC
- [ ] Update database access to EF Core
- [ ] Implement dependency injection

### Phase 6: Add Testing
- [ ] Set up unit testing project (xUnit or NUnit)
- [ ] Write tests for services
- [ ] Write integration tests for database operations
- [ ] Add end-to-end tests

## Breaking Changes

### Bootstrap 5 Migration
Bootstrap 5 has breaking changes from Bootstrap 3:
- Dropdown markup has changed
- Some classes have been renamed
- jQuery is no longer required

**Action Required:** Review and update the following files:
- `Home.Master` - Navigation dropdowns need updating
- All `.aspx` files using Bootstrap classes

### jQuery Update
jQuery 3.x has deprecated some methods:
- `.bind()` → use `.on()`
- `.delegate()` → use `.on()`
- `.live()` → use `.on()`

**Action Required:** Review `js/bbq.js` for deprecated jQuery methods

## Security Checklist

- [x] Remove hardcoded credentials
- [x] Enable custom errors in production
- [x] Add security headers
- [x] Force HTTPS
- [ ] Implement input validation on all forms
- [ ] Add CSRF protection
- [ ] Implement rate limiting for contact form
- [ ] Add SQL injection protection review
- [ ] Implement proper authentication/authorization
- [ ] Add Content Security Policy (CSP)

## Performance Improvements (Future)

- [ ] Enable output caching
- [ ] Implement bundling and minification
- [ ] Optimize images
- [ ] Add CDN for static assets
- [ ] Implement lazy loading
- [ ] Add database query optimization

## Notes

- The old `jquery-1.2.6.js` file is still in the project but no longer referenced
- MySQL reference in project is unused (only SQL Server is used)
- Some pages may need Bootstrap markup updates to work with Bootstrap 5
