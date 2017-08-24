# EventLog #

## What ##

An audit trail for Sitecore.

## Compatibility ##

The module was created and tested on Sitecore 8.1 update-3.

## Usage ##

### Installation ###

The module is made available on the Sitecore marketplace as a Sitecore package. 
The package includes:

- a config file that includes commands, event handlers and settings
- the dll

Some actions required on SQL:

- Create a new Database, e.g. "<ProjectName>-AuditTrail"
- Executed the script "DB Schema Script.sql" on the new database
- In your ConnectionStrings.config, make a new connectionstring "SCAuditTrail"
 
## History ##
- v1.0 : initial release
