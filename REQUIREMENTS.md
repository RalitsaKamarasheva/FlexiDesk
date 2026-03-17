## 📋 User Stories & Business Requirements

The following requirements guide the development of the **FlexiDesk** system, ensuring it meets the needs of both the business and its users.

| Role | Requirement | Benefit |
| :--- | :--- | :--- |
| **Member** | As a member, I want to filter available desks by date and time | So I can plan my work week effectively. |
| **Member** | As a member, I want to see my upcoming reservations | So I can manage my schedule. |
| **Admin** | As an admin, I want to add or disable specific desks/rooms | So I can manage the office capacity. |
| **System** | As a system, I must prevent double-booking the same resource | To avoid conflicts and customer dissatisfaction. |

---

### Key Business Rules
* **No Overlapping:** A resource (desk/room) cannot be booked by two different users for the same time interval.
* **Future Dating:** Reservations can only be made for future dates and times.
* **Resource Status:** Disabled or "Out of Order" resources are automatically excluded from the search results.
