document.addEventListener('DOMContentLoaded', () => {
    fetchContacts();
});

interface Contact {
    ContactId: number;
    Name: string;
    Email: string;
    Mobile: string;
    Landline: string;
    Website: string;
    Address: string;
}

function fetchContacts() {
    fetch("http://localhost:5140/").then(response => response.json())
        .then(contacts => {
            renderData(contacts as Contact[]);
        })
        .catch(error => console.error('Error:', error));
}

function renderData(contacts: Contact[]) {
    console.log(contacts);
    const table = document.getElementById('contacts-table');

    if (table) {
        contacts.forEach(contact => {
            const row = document.createElement('tr');
            row.setAttribute("data-contact-id", String(contact['contactId' as keyof Contact]));
            for (const key in contact) {
                if (String(key) != "contactId") {
                    const td = document.createElement('td');
                    if (String(key) == "name") {
                        td.setAttribute("class", "contact-name");
                    }
                    td.textContent = String(contact[String(key) as keyof Contact]);
                    row.appendChild(td);
                }
            }
            table.appendChild(row);
        });
    }
}



function submitContactForm(event: Event) {
    const form = event.target as HTMLFormElement;
    const formData = new FormData(form);
    const formDataObject: Record<string, string> = {};
    formData.forEach((value, key) => {
        formDataObject[key] = value as string;
    });
    const options: RequestInit = {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formDataObject),
    };
    fetch('http://localhost:5140/Data', options)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => console.log(data))
        .catch(error => console.error('Error:', error));
}

const form = document.getElementById('add-contact');

if (form) {
    form.addEventListener('submit', submitContactForm);
}