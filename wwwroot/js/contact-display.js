
document.addEventListener('DOMContentLoaded', () => {
    fetchContact();
});

function fetchContact() {
    const urlParams = new URLSearchParams(window.location.search);
    const contactId = urlParams.get('id');
    fetch("http://localhost:5140/api/contacts/" + contactId).then(response => response.json())
        .then(contact => { displayContact(contact); })
        .catch(error => console.error('Error:', error));
}

function displayContact(contact) {
    const contactSection = document.getElementsByClassName("display-contact")[0];
    console.log(contact["name"])
    for (const key in contact) {
        if (key != "contactId") {
            const div = document.createElement("div");
            const p = document.createElement("p");
            if (key == "name") {
                p.setAttribute("class", "name");
                p.textContent = String(contact[key]);
            }
            else {
                p.textContent = key + ":" + String(contact[key]);
            }
            div.appendChild(p);
            contactSection.appendChild(div);
        }
    }
    focusContact();
    editFormData(contact);
}

function focusContact() {
    const urlParams = new URLSearchParams(window.location.search);
    const contactId = urlParams.get('id');
    const elements = document.querySelectorAll('tr');
    elements.forEach(element => {
        if (element.getAttribute("data-contact-id") == contactId.toString()) {
            element.setAttribute("class", "focus-contact");
        }
    });
}

function editFormData(contact) {
    const editForm = document.getElementById("edit-form");
    if (editForm) {
        for (const key in contact) {
            if (key != "contactId") {
                const ele = editForm.querySelector("input[id=" + key.charAt(0).toUpperCase() + key.slice(1) + "]");
                if (ele) {
                    ele.value = String(contact[key]);
                }
            }
        }
    }
}

function editContact() {
    const editForm = document.getElementById("edit-form");
    if (editForm) {
        editForm.style.display = "flex";
    }
}

function deleteContact() {
    const urlParams = new URLSearchParams(window.location.search);
    const contactId = urlParams.get('id');
    const options = {
        method: "DELETE",
    };
    fetch("http://localhost:5140/api/contacts/" + contactId, options)
        .then(response => {
            if (response.status == 204) {
                window.location.href = "home.html"
            } else {
                console.error('Unexpected status code:', response.status);
            }
        })
        .catch(error => console.error('Error:', error));
}

function updateContactForm(event) {
    event.preventDefault();
    const editForm = event.target;
    const urlParams = new URLSearchParams(window.location.search);
    const contactId = urlParams.get('id');
    const formData = new FormData(editForm);
    const formDataObject = {};
    formData.forEach((value, key) => {
        formDataObject[key] = value;
    });
    const options = {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formDataObject),
    };
    fetch('http://localhost:5140/api/contacts/' + contactId, options)
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
                displayErrors(editForm,errors);
            }
        })
        .catch(error => {
            console.error('Fetch Error:', error.message);
        });
}

const editForm = document.getElementById('edit-form');
if (editForm) {
    editForm.addEventListener('submit', updateContactForm);
}

function redirect() {
    window.location.href = "home.html";
}