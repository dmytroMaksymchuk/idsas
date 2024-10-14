import { useState } from 'react';
import './App.css';

function App() {
    const [selectedFile, setSelectedFile] = useState(null);
    const [fileLink, setFileLink] = useState('');

    // Handle file selection
    const handleFileChange = (event) => {
        setSelectedFile(event.target.files[0]);
    };
    const handleLinkChange = (event) => {
        setFileLink(event.target.value);
    };

    // Handle file upload (this can be customized to your API or upload logic)
    const handleFileUpload = () => {
        if (selectedFile) {
            // Create a FormData object to send the file to the server
            const formData = new FormData();
            formData.append('file', selectedFile);

            // Send the file to the server
            // fetch('/upload', {
            //     method: 'POST',
            //     body: formData,
            // })
            // .then((response) => response.json())
            // .then((data) => {
            //     console.log('File uploaded successfully:', data);
            // })
            // .catch((error) => {
            //     console.error('Error uploading file:', error);
            // });
        } else {
            console.log('No file selected');
        }
    };

    const handleViewFile = () => {
        // TODO: Implement file viewing logic
        console.log('Viewing file:', fileLink);
    };

    return (
        <div className="App">
            <div className={"file-uploading-container"}>
                <h2>Upload a File</h2>
                <input type="file" onChange={handleFileChange}/>
                <div className={"button-group"}>
                    <button className={"verify-button"} onClick={handleFileUpload}>Verify</button>
                    <button className={"sign-button"} onClick={handleFileUpload}>Sign</button>
                </div>

            </div>
            <div className="enter-link-container">
                <h2>Enter File Link</h2>
                <input
                    type="text"
                    className={"link-input"}
                    placeholder="Enter the file link"
                    value={fileLink}
                    onChange={handleLinkChange}
                />
                <button onClick={handleViewFile}>View File</button>
            </div>
        </div>
    );
}

export default App;
