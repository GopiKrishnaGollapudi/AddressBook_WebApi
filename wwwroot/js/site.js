function EnableForm() {
    var element = document.getElementsByClassName("contact-form")[0];
    element.style.display = "flex";
}

document.addEventListener('DOMContentLoaded', () => {
    fetchContacts();
});

function fetchContacts() {
    let options = {
        headers: {
            "User-Agent": "Request"
        }
    };
    fetch("http://localhost:5140/api/contacts/").then(response => response.json())
        .then(contacts => {
            renderData(contacts);
        })
        .catch(error => console.error('Error:', error));
}

function renderData(contacts) {
    const table = document.getElementById('contacts-table');
    if (table) {
        contacts.forEach(contact => {
            const row = document.createElement('tr');
            row.setAttribute("data-contact-id", String(contact['contactId']));
            row.addEventListener("click", getContact);
            for (const key in contact) {
                if (String(key) != "contactId") {
                    const td = document.createElement('td');
                    if (String(key) == "name") {
                        td.setAttribute("class", "contact-name");
                    }
                    td.textContent = String(contact[String(key)]);
                    row.appendChild(td);
                }
            }
            table.appendChild(row);
        });
    }
}

function submitContactForm(event) {
    event.preventDefault();
    const form = event.target;
    const formData = new FormData(form);
    const formDataObject = {};
    formData.forEach((value, key) => {
        formDataObject[key] = value;
    });
    const options = {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formDataObject),
    };
    fetch('http://localhost:5140/api/contacts/', options)
        .then(response => {
            if (response.status == 204) {
                console.log(response.body);
                window.location.href = "home.html";
            }
            else if (response.status == 400) {
                return response.json();
            }
        })
        .then(errors => {
            if (errors) {
                displayErrors(form,errors);
            }
        })
        .catch(error => {
            console.error('Fetch Error:', error.message);
        });
}

function displayErrors(formElement,errors) {
    const formErrors = errors.errors;
    if (form) {
        for (const key in formErrors) {
            if (key != "contactId") {
                const ele = formElement.querySelector("span[id=" + key.charAt(0).toUpperCase() + key.slice(1) + "]");
                ele.textContent = String(formErrors[key]);
            }
        }
    }
}


const form = document.getElementById('add-contact');

if (form) {
    form.addEventListener('submit', submitContactForm);
}

function getContact(event) {
    const contact = event.currentTarget;
    const contactId = contact.getAttribute("data-contact-id");
    window.location.href = "contacts-display.html?id=" + contactId;
}


