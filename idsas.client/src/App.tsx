import {useState} from 'react';
import './App.css';
import Header from "./components/Header.tsx";
import UploadFilePopUp from "./components/UploadFilePopUp.tsx";
import MyDocumentsList, {ShareState, MyDocument} from "./components/MyDocumentList.tsx";
import SharedDocumentList, {SharedDocument} from "./components/SharedDocumentList.tsx";

function App() {
    const [selectedFile, setSelectedFile] = useState(null);
    const [uploadFileActive, setUploadFileActive] = useState(false);

    const [myDocuments, setMyDocuments] = useState<MyDocument[]>([
        { name: 'Document1.pdf', shareState: ShareState.shared },
        { name: 'Document2.docx', shareState: ShareState.private },
        { name: 'Presentation.pptx', shareState: ShareState.waitingConfirmation },
    ]);
    
    const [sharedDocuments, setSharedDocuments] = useState<SharedDocument[]>([
        { name: 'Document3.pdf' },
        { name: 'Document4.docx' },
        { name: 'Presentation2.pptx' },
    ]);

    const handleFileChange = (event) => {
        setSelectedFile(event.target.files[0]);
    };
    const onDownloadFile = (index: number) => {
        console.log('Downloading file:', index);
    }

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

    return (
        <div className="App">
            <Header uploadFileActive={uploadFileActive} setUploadFileActive={setUploadFileActive}/>
            {uploadFileActive &&
                <UploadFilePopUp handleFileChange={handleFileChange} onVerify={() => console.log("verify")} onSign={() => console.log("sign")} setUploadFileActive={setUploadFileActive}/>
            }
            {!uploadFileActive &&
                <>
                    <MyDocumentsList title={"My Documents"} documents={myDocuments} onDownloadFile={onDownloadFile} />
                    <SharedDocumentList title={"Documents Shared With Me"} documents={sharedDocuments} onDownloadFile={() => console.log("Download")} />
                </>
            }
        </div>
    );
}

export default App;
